plugins {
    id("org.hidetake.swagger.generator") version "2.19.2"
}

group = "com.xecta.platform.data.client.python"
version = System.getenv("BUILD_BUILDNUMBER") ?: "0.0.1"

val dockerRepository: String = System.getenv("DOCKER_REGISTRY") ?: "675249268777.dkr.ecr.us-east-1.amazonaws.com"

repositories {
    mavenCentral()
}

dependencies {
    dependencies {
        swaggerCodegen("org.openapitools:openapi-generator-cli:5.0.0")    // or OpenAPI Generator.
    }
}

tasks {
    swaggerSources {
        create("dataclient-python") {
            setInputFile(file("schemas/production-oas.json"))
            code.language = "python"
            code.outputDir = file("python/generated-source")
        }
    }

    swaggerSources {
        create("dataclient-kotlin") {
            setInputFile(file("schemas/production-oas.json"))
            code.language = "kotlin"
            code.outputDir = file("kotlin/generated-source")
        }
    }

    swaggerSources {
        create("dataclient-csharp") {
            setInputFile(file("schemas/production-oas.json"))
            code.language = "csharp-netcore"
            code.outputDir = file("csharp/generated-source")
        }
    }
}