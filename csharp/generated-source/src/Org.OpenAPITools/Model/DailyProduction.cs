/*
 * Production API
 *
 * API exposing endpoints for managing well headers and daily production.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// DailyProduction
    /// </summary>
    [DataContract(Name = "DailyProduction")]
    public partial class DailyProduction : IEquatable<DailyProduction>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyProduction" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DailyProduction() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyProduction" /> class.
        /// </summary>
        /// <param name="xid">xid.</param>
        /// <param name="uwi">uwi (required).</param>
        /// <param name="datetime">datetime (required).</param>
        /// <param name="liquidRate">liquidRate (required).</param>
        /// <param name="oilProductionRate">oilProductionRate (required).</param>
        /// <param name="gasProductionRate">gasProductionRate (required).</param>
        /// <param name="waterProductionRate">waterProductionRate (required).</param>
        /// <param name="choke">choke (required).</param>
        /// <param name="gasOilRatio">gasOilRatio (required).</param>
        /// <param name="waterCut">waterCut (required).</param>
        /// <param name="tubingPressure">tubingPressure (required).</param>
        /// <param name="casingPressure">casingPressure (required).</param>
        /// <param name="gasInjectionRate">gasInjectionRate (required).</param>
        /// <param name="operatingFrequency">operatingFrequency (required).</param>
        /// <param name="strokesPerMinute">strokesPerMinute (required).</param>
        /// <param name="downtimeHours">downtimeHours (required).</param>
        /// <param name="downtimeCode">downtimeCode (required).</param>
        public DailyProduction(Guid xid = default(Guid), string uwi = default(string), DateTime datetime = default(DateTime), double liquidRate = default(double), double oilProductionRate = default(double), double gasProductionRate = default(double), double waterProductionRate = default(double), double choke = default(double), double gasOilRatio = default(double), double waterCut = default(double), double tubingPressure = default(double), double casingPressure = default(double), double gasInjectionRate = default(double), double operatingFrequency = default(double), double strokesPerMinute = default(double), double downtimeHours = default(double), int downtimeCode = default(int))
        {
            // to ensure "uwi" is required (not null)
            this.Uwi = uwi ?? throw new ArgumentNullException("uwi is a required property for DailyProduction and cannot be null");
            this.Datetime = datetime;
            this.LiquidRate = liquidRate;
            this.OilProductionRate = oilProductionRate;
            this.GasProductionRate = gasProductionRate;
            this.WaterProductionRate = waterProductionRate;
            this.Choke = choke;
            this.GasOilRatio = gasOilRatio;
            this.WaterCut = waterCut;
            this.TubingPressure = tubingPressure;
            this.CasingPressure = casingPressure;
            this.GasInjectionRate = gasInjectionRate;
            this.OperatingFrequency = operatingFrequency;
            this.StrokesPerMinute = strokesPerMinute;
            this.DowntimeHours = downtimeHours;
            this.DowntimeCode = downtimeCode;
            this.Xid = xid;
        }

        /// <summary>
        /// Gets or Sets Xid
        /// </summary>
        [DataMember(Name = "xid", EmitDefaultValue = false)]
        public Guid Xid { get; set; }

        /// <summary>
        /// Gets or Sets Uwi
        /// </summary>
        [DataMember(Name = "uwi", IsRequired = true, EmitDefaultValue = false)]
        public string Uwi { get; set; }

        /// <summary>
        /// Gets or Sets Datetime
        /// </summary>
        [DataMember(Name = "datetime", IsRequired = true, EmitDefaultValue = false)]
        public DateTime Datetime { get; set; }

        /// <summary>
        /// Gets or Sets LiquidRate
        /// </summary>
        [DataMember(Name = "liquidRate", IsRequired = true, EmitDefaultValue = false)]
        public double LiquidRate { get; set; }

        /// <summary>
        /// Gets or Sets OilProductionRate
        /// </summary>
        [DataMember(Name = "oilProductionRate", IsRequired = true, EmitDefaultValue = false)]
        public double OilProductionRate { get; set; }

        /// <summary>
        /// Gets or Sets GasProductionRate
        /// </summary>
        [DataMember(Name = "gasProductionRate", IsRequired = true, EmitDefaultValue = false)]
        public double GasProductionRate { get; set; }

        /// <summary>
        /// Gets or Sets WaterProductionRate
        /// </summary>
        [DataMember(Name = "waterProductionRate", IsRequired = true, EmitDefaultValue = false)]
        public double WaterProductionRate { get; set; }

        /// <summary>
        /// Gets or Sets Choke
        /// </summary>
        [DataMember(Name = "choke", IsRequired = true, EmitDefaultValue = false)]
        public double Choke { get; set; }

        /// <summary>
        /// Gets or Sets GasOilRatio
        /// </summary>
        [DataMember(Name = "gasOilRatio", IsRequired = true, EmitDefaultValue = false)]
        public double GasOilRatio { get; set; }

        /// <summary>
        /// Gets or Sets WaterCut
        /// </summary>
        [DataMember(Name = "waterCut", IsRequired = true, EmitDefaultValue = false)]
        public double WaterCut { get; set; }

        /// <summary>
        /// Gets or Sets TubingPressure
        /// </summary>
        [DataMember(Name = "tubingPressure", IsRequired = true, EmitDefaultValue = false)]
        public double TubingPressure { get; set; }

        /// <summary>
        /// Gets or Sets CasingPressure
        /// </summary>
        [DataMember(Name = "casingPressure", IsRequired = true, EmitDefaultValue = false)]
        public double CasingPressure { get; set; }

        /// <summary>
        /// Gets or Sets GasInjectionRate
        /// </summary>
        [DataMember(Name = "gasInjectionRate", IsRequired = true, EmitDefaultValue = false)]
        public double GasInjectionRate { get; set; }

        /// <summary>
        /// Gets or Sets OperatingFrequency
        /// </summary>
        [DataMember(Name = "operatingFrequency", IsRequired = true, EmitDefaultValue = false)]
        public double OperatingFrequency { get; set; }

        /// <summary>
        /// Gets or Sets StrokesPerMinute
        /// </summary>
        [DataMember(Name = "strokesPerMinute", IsRequired = true, EmitDefaultValue = false)]
        public double StrokesPerMinute { get; set; }

        /// <summary>
        /// Gets or Sets DowntimeHours
        /// </summary>
        [DataMember(Name = "downtimeHours", IsRequired = true, EmitDefaultValue = false)]
        public double DowntimeHours { get; set; }

        /// <summary>
        /// Gets or Sets DowntimeCode
        /// </summary>
        [DataMember(Name = "downtimeCode", IsRequired = true, EmitDefaultValue = false)]
        public int DowntimeCode { get; set; }

        /// <summary>
        /// Gets or Sets CreatedAt
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Returns false as CreatedAt should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets CreatedBy
        /// </summary>
        [DataMember(Name = "createdBy", EmitDefaultValue = false)]
        public string CreatedBy { get; private set; }

        /// <summary>
        /// Returns false as CreatedBy should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCreatedBy()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets UpdatedAt
        /// </summary>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Returns false as UpdatedAt should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets UpdatedBy
        /// </summary>
        [DataMember(Name = "updatedBy", EmitDefaultValue = false)]
        public string UpdatedBy { get; private set; }

        /// <summary>
        /// Returns false as UpdatedBy should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUpdatedBy()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets Version
        /// </summary>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = false)]
        public int Version { get; private set; }

        /// <summary>
        /// Returns false as Version should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeVersion()
        {
            return false;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DailyProduction {\n");
            sb.Append("  Xid: ").Append(Xid).Append("\n");
            sb.Append("  Uwi: ").Append(Uwi).Append("\n");
            sb.Append("  Datetime: ").Append(Datetime).Append("\n");
            sb.Append("  LiquidRate: ").Append(LiquidRate).Append("\n");
            sb.Append("  OilProductionRate: ").Append(OilProductionRate).Append("\n");
            sb.Append("  GasProductionRate: ").Append(GasProductionRate).Append("\n");
            sb.Append("  WaterProductionRate: ").Append(WaterProductionRate).Append("\n");
            sb.Append("  Choke: ").Append(Choke).Append("\n");
            sb.Append("  GasOilRatio: ").Append(GasOilRatio).Append("\n");
            sb.Append("  WaterCut: ").Append(WaterCut).Append("\n");
            sb.Append("  TubingPressure: ").Append(TubingPressure).Append("\n");
            sb.Append("  CasingPressure: ").Append(CasingPressure).Append("\n");
            sb.Append("  GasInjectionRate: ").Append(GasInjectionRate).Append("\n");
            sb.Append("  OperatingFrequency: ").Append(OperatingFrequency).Append("\n");
            sb.Append("  StrokesPerMinute: ").Append(StrokesPerMinute).Append("\n");
            sb.Append("  DowntimeHours: ").Append(DowntimeHours).Append("\n");
            sb.Append("  DowntimeCode: ").Append(DowntimeCode).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  Version: ").Append(Version).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as DailyProduction);
        }

        /// <summary>
        /// Returns true if DailyProduction instances are equal
        /// </summary>
        /// <param name="input">Instance of DailyProduction to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DailyProduction input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Xid == input.Xid ||
                    (this.Xid != null &&
                    this.Xid.Equals(input.Xid))
                ) && 
                (
                    this.Uwi == input.Uwi ||
                    (this.Uwi != null &&
                    this.Uwi.Equals(input.Uwi))
                ) && 
                (
                    this.Datetime == input.Datetime ||
                    (this.Datetime != null &&
                    this.Datetime.Equals(input.Datetime))
                ) && 
                (
                    this.LiquidRate == input.LiquidRate ||
                    this.LiquidRate.Equals(input.LiquidRate)
                ) && 
                (
                    this.OilProductionRate == input.OilProductionRate ||
                    this.OilProductionRate.Equals(input.OilProductionRate)
                ) && 
                (
                    this.GasProductionRate == input.GasProductionRate ||
                    this.GasProductionRate.Equals(input.GasProductionRate)
                ) && 
                (
                    this.WaterProductionRate == input.WaterProductionRate ||
                    this.WaterProductionRate.Equals(input.WaterProductionRate)
                ) && 
                (
                    this.Choke == input.Choke ||
                    this.Choke.Equals(input.Choke)
                ) && 
                (
                    this.GasOilRatio == input.GasOilRatio ||
                    this.GasOilRatio.Equals(input.GasOilRatio)
                ) && 
                (
                    this.WaterCut == input.WaterCut ||
                    this.WaterCut.Equals(input.WaterCut)
                ) && 
                (
                    this.TubingPressure == input.TubingPressure ||
                    this.TubingPressure.Equals(input.TubingPressure)
                ) && 
                (
                    this.CasingPressure == input.CasingPressure ||
                    this.CasingPressure.Equals(input.CasingPressure)
                ) && 
                (
                    this.GasInjectionRate == input.GasInjectionRate ||
                    this.GasInjectionRate.Equals(input.GasInjectionRate)
                ) && 
                (
                    this.OperatingFrequency == input.OperatingFrequency ||
                    this.OperatingFrequency.Equals(input.OperatingFrequency)
                ) && 
                (
                    this.StrokesPerMinute == input.StrokesPerMinute ||
                    this.StrokesPerMinute.Equals(input.StrokesPerMinute)
                ) && 
                (
                    this.DowntimeHours == input.DowntimeHours ||
                    this.DowntimeHours.Equals(input.DowntimeHours)
                ) && 
                (
                    this.DowntimeCode == input.DowntimeCode ||
                    this.DowntimeCode.Equals(input.DowntimeCode)
                ) && 
                (
                    this.CreatedAt == input.CreatedAt ||
                    (this.CreatedAt != null &&
                    this.CreatedAt.Equals(input.CreatedAt))
                ) && 
                (
                    this.CreatedBy == input.CreatedBy ||
                    (this.CreatedBy != null &&
                    this.CreatedBy.Equals(input.CreatedBy))
                ) && 
                (
                    this.UpdatedAt == input.UpdatedAt ||
                    (this.UpdatedAt != null &&
                    this.UpdatedAt.Equals(input.UpdatedAt))
                ) && 
                (
                    this.UpdatedBy == input.UpdatedBy ||
                    (this.UpdatedBy != null &&
                    this.UpdatedBy.Equals(input.UpdatedBy))
                ) && 
                (
                    this.Version == input.Version ||
                    this.Version.Equals(input.Version)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Xid != null)
                    hashCode = hashCode * 59 + this.Xid.GetHashCode();
                if (this.Uwi != null)
                    hashCode = hashCode * 59 + this.Uwi.GetHashCode();
                if (this.Datetime != null)
                    hashCode = hashCode * 59 + this.Datetime.GetHashCode();
                hashCode = hashCode * 59 + this.LiquidRate.GetHashCode();
                hashCode = hashCode * 59 + this.OilProductionRate.GetHashCode();
                hashCode = hashCode * 59 + this.GasProductionRate.GetHashCode();
                hashCode = hashCode * 59 + this.WaterProductionRate.GetHashCode();
                hashCode = hashCode * 59 + this.Choke.GetHashCode();
                hashCode = hashCode * 59 + this.GasOilRatio.GetHashCode();
                hashCode = hashCode * 59 + this.WaterCut.GetHashCode();
                hashCode = hashCode * 59 + this.TubingPressure.GetHashCode();
                hashCode = hashCode * 59 + this.CasingPressure.GetHashCode();
                hashCode = hashCode * 59 + this.GasInjectionRate.GetHashCode();
                hashCode = hashCode * 59 + this.OperatingFrequency.GetHashCode();
                hashCode = hashCode * 59 + this.StrokesPerMinute.GetHashCode();
                hashCode = hashCode * 59 + this.DowntimeHours.GetHashCode();
                hashCode = hashCode * 59 + this.DowntimeCode.GetHashCode();
                if (this.CreatedAt != null)
                    hashCode = hashCode * 59 + this.CreatedAt.GetHashCode();
                if (this.CreatedBy != null)
                    hashCode = hashCode * 59 + this.CreatedBy.GetHashCode();
                if (this.UpdatedAt != null)
                    hashCode = hashCode * 59 + this.UpdatedAt.GetHashCode();
                if (this.UpdatedBy != null)
                    hashCode = hashCode * 59 + this.UpdatedBy.GetHashCode();
                hashCode = hashCode * 59 + this.Version.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
