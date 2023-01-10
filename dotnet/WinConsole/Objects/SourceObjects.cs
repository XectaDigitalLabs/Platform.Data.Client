using Xecta.Data.OpenApi.Client.Model;

namespace Objects;

/// <summary>
/// Wrapper around API WellInput object \n
/// Adds convenient collection(s) for wellbore and daily production
/// Need parameterless constructor to make used of reflection and Json conversion
/// </summary>
public class SourceWell : WellInput
{
    /// <summary>
    /// Represents a well from a customer source database
    /// </summary>
    public SourceWell() { }

    /// <summary>
    /// List of wellbores related to this well
    /// (there must be at least 1 bore)
    /// </summary>
    public List<SourceWellBore> WellBores { get; set; } = new List<SourceWellBore>();
    /// <summary>
    /// List of daily production values for each production day 
    /// (should be 1 record for each producing day in life of well)
    /// </summary>
    public List<SourceDailyProduction> DailyProduction { get; set; } = new List<SourceDailyProduction>();

    /// <summary>
    /// Filter to return last 90 days of production
    /// </summary>
    /// <returns>Last 90 days of production history</returns>
    public List<SourceDailyProduction> GetDailyProductionLast90Days()
    {
        var batch = DailyProduction.Where(
            X => X.Date >= DateTime.Today.AddDays(-90))
            .ToList().OrderBy(x => x.Date).ToList();
        return batch;
    }
    /// <summary>
    /// Batches daily production history into smaller 1000 record batches
    /// The api has 10 mb size limit per each push request. 
    /// Large requests > 1000 records will typically fail because they are too large
    /// </summary>
    /// <returns>1-N batches of Max 1000 records</returns>
    public Dictionary<int, List<SourceDailyProduction>> GetDailyProductionBatches()
    {
        var size = 1000;
        var batches = new Dictionary<int, List<SourceDailyProduction>>();
        var arr = DailyProduction.ToArray();
        for (var i = 0; i < arr.Length / size + 1; i++)
        {
            batches.Add(i, arr.Skip(i * size).Take(size).ToList());
        }
        return batches;

    }
}

/// <summary>
/// Wrapper wround API wellbore input. 
/// Adds Lists of X for the various collections related to the wellbore 
/// (deviation survey, casing, tubing etc)
/// </summary>
public class SourceWellBore : WellboreInput
{
    public SourceWellBore()
    { }
    public static SourceWellBore CreateDefault(SourceWell well)
    {
        var bore = new SourceWellBore();
        bore.SourceId = well.SourceId;
        bore.SourceWellId = well.SourceId;
        bore.Name = well.Name + "-WellBore";
        return bore;
    }
    /// <summary>
    /// The formation info (likely to be sparsely populated)
    /// </summary>
    public SourceFormation Formation { get; set; } = new SourceFormation();
    /// <summary>
    /// The Drilled hole deviation survey
    /// </summary>
    public IEnumerable<SourceDeviationSurvey> DeviationsSurveys { get; set; } = new List<SourceDeviationSurvey>();
    /// <summary>
    /// How the bore was cased 
    /// </summary>
    public IEnumerable<SourceCasing> Casings { get; set; } = new List<SourceCasing>();
    /// <summary>
    /// How the bore was completed
    /// </summary>
    public IEnumerable<SourceTubing> Tubings { get; set; } = new List<SourceTubing>();

}

/// <summary>
/// Wrapper around the API FormationInput object.
/// Parameterless for JSON and reflection needs
/// </summary>
public class SourceFormation : FormationInput
{
    public SourceFormation()
    {
        base.AllocationFactor = 100;
        base.CompressibilityRock = 0;
        base.PressureFormationInitialDatum = 0;
        base.Porosity = 0;
        base.FluidComingledGOR = 0;
        base.FluidGravityApi = 0;
        base.FluidGravityGas = 0;
        base.FluidMolarFracCO2 = 0;
        base.FluidMolarFracH2S = 0;
        base.FluidMolarFracN2 = 0;
        base.FluidSalinityWater = 0;
        base.PrimaryFluidType = PrimaryFluidTypeEnum.OIL;
        base.Rsi = 0;
        base.SaturationWaterInitial = 0;
        base.SaturationOilInitial = 0;
        base.SaturationGasInitial = 0;
        base.TemperatureFormationDatum = 0;
        base.ThicknessFormation = 0;
        base.VolumeAcquiferInitial = 0;
    }
    /// <summary>
    /// Set the default fluid
    /// </summary>
    /// <param name="upperCaseFluid">OIL, GAS, WATER (as string)</param>
    public void SetPrimaryFluid(string upperCaseFluid)
    {
        PrimaryFluidTypeEnum fluid;
        Enum.TryParse(upperCaseFluid, out fluid);
        base.PrimaryFluidType = fluid;
    }

}

/// <summary>
/// Wrapper around the API Deviation Surey object.
/// Parameterless for JSON and reflection needs
/// </summary>
public class SourceDeviationSurvey : DeviationSurveyInput
{
    public SourceDeviationSurvey()
    { }
}

/// <summary>
/// Wrapper around the API CasingInput object.
/// Parameterless for JSON and reflection needs
/// </summary>
public class SourceCasing : CasingInput
{
    public SourceCasing()
    {
        base.TopMd = 0;
        base.BottomMd = 0;
        base.Roughness = 0;
        base.Id = 0;
        base.Od = 0;
        base.RunDate = DateTime.MinValue;
        base.SourceId = "None";
    }
}

/// <summary>
/// Wrapper around the API TubingInput object.
/// Parameterless for JSON and reflection needs
/// </summary>
public class SourceTubing : TubingInput
{
    public SourceTubing()
    {

        base.TopMd = 0;
        base.BottomMd = 0;
        base.Roughness = 0;
        base.Id = 0;
        base.Od = 0;
        base.RunDate = DateTime.MinValue;
        base.SourceId = "None";
    }
}


/// <summary>
/// Wrapper around API daily production. 
/// Need parameterless constructor to make used of reflection and Json conversion
/// </summary>
public class SourceDailyProduction : DailyProductionInput
{
    public SourceDailyProduction()
    {
        base.Uwi = string.Empty;
        base.Date = DateTime.MinValue;
        base.OilRate = 0;
        base.GasRate = 0;
        base.WaterRate = 0;
        base.Choke = 0;
        base.TubingPressure = 0;
        base.WellheadTemperature = 0;
        base.CasingPressure = 0;
        base.DowntimeHours = 0;
        base.DowntimeCode = "None";

    }
}

