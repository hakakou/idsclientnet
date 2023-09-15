using System;

namespace IDSApi.Test;

public class IDSApiTests
{
    [Test, Order(1)]
    public async Task Test1()
    {
        //## Register Resource

        var client = Client.CreateClient("https://ids.co2path.io:8080");

        var offeredResourceDesc = Newtonsoft.Json.JsonConvert.DeserializeObject<OfferedResourceDesc>(
          System.IO.File.ReadAllText("Input/01-RegisterResource.json"));

        var registerResourceRes = await client.ApiOffersPostAsync(offeredResourceDesc);
        Console.WriteLine("Register Resource " + registerResourceRes.Dump());
        Console.WriteLine("------");

        var offers = new Uri[] { new Uri(registerResourceRes._links["self"].Href) };
        var offers2 = offers.Select(q => new Uri(q.AbsoluteUri.Replace("ids.co2path.io", "connectora"))).ToArray();
        var offersId = Guid.Parse(registerResourceRes._links["self"].Href[^36..]);

        //## Create Catalog

        var catalogDesc = new CatalogDesc()
        {
            Title = "IDS Catalog",
            Description = "This catalog is created from an IDS infomodel catalog."
        };

        var createCatalogReq = await client.ApiCatalogsPostAsync(catalogDesc);
        Console.WriteLine("Create Catalog " + createCatalogReq.Dump());
        Console.WriteLine("------");
        var catalogId = Guid.Parse(createCatalogReq._links["self"].Href[^36..]);

        //## Add Offer to catalog

        var oreq = await client.ApiCatalogsOffersPostAsync(catalogId, offers);

        Console.WriteLine("Add Offer to catalog " + oreq.Dump());
        Console.WriteLine("------");

        //## Create Rule
        var orule = await client.ApiRulesPostAsync(new ContractRuleDesc()
        {
            Title = "Example Usage Policy",
            Description = "Usage policy provide access applied",
            Value = "{\n  \"@context\" : {\n    \"ids\" : \"https://w3id.org/idsa/core/\",\n    \"idsc\" : \"https://w3id.org/idsa/code/\"\n  },\n  \"@type\" : \"ids:Permission\",\n  \"@id\" : \"https://w3id.org/idsa/autogen/permission/51f5f7e4-f97f-4f91-bc57-b243714642be\",\n  \"ids:description\" : [ {\n    \"@value\" : \"Usage policy provide access applied\",\n    \"@type\" : \"http://www.w3.org/2001/XMLSchema#string\"\n  } ],\n  \"ids:title\" : [ {\n    \"@value\" : \"Example Usage Policy\",\n    \"@type\" : \"http://www.w3.org/2001/XMLSchema#string\"\n  } ],\n    \"ids:action\" : [ {\n    \"@id\" : \"https://w3id.org/idsa/code/USE\"\n  } ]\n }"
        });

        var rules = new Uri[] { new Uri(orule._links["self"].Href) };
        // var ruleId = Guid.Parse(createCatalogReq._links["self"].Href[^36..]);

        Console.WriteLine("Create Rule " + orule.Dump());
        Console.WriteLine("------");

        //## Generate Contract Template
        var getreq = await client.ApiContractsPostAsync(new ContractDesc()
        {
            Title = "CO2Path Contract",
            Provider = new Uri("https://connectora:8080"),

            // "Request Information regarding the desired resource" return 417 if consumer is used.
            // Consumer = new Uri("https://www.co2path.io"),

            // Ensure valid dates
            Start = DateTime.Now.AddYears(-1),
            End = DateTime.Now.AddYears(+10),
        });

        Console.WriteLine("Generate Contract Template " + getreq.Dump());
        Console.WriteLine("------");
        var contracts = new Uri[] { new Uri(getreq._links["self"].Href) };
        var contractId = Guid.Parse(getreq._links["self"].Href[^36..]);

        //## Add Rule to Contract Template: POST /api/contracts/{id}/rules

        var addres = await client.ApiContractsRulesPostAsync(contractId, rules);
        Console.WriteLine("Add Rule to Contract " + addres.Dump());
        Console.WriteLine("------");

        //## Artifacts: POST /api/artifacts

        var report = new
        {
            Origin = new { Lat = 29000, Lng = 33000 },
            Destination = new { Lat = 28000, Lng = 32000 },
            Timestamp = DateTime.Now,
            Shipment = new
            {
                CargoType = 1,
                DistanceKm = 1000,
                EmptyTripFactor = 30,
                FuelL = 1000,
                LoadingFactor = 90,
                PayloadT = 22,
                VehicleType = 2,
            },
            Emissions = new
            {
                Co2eTtw = 10,
                Co2eWtw = 20,
                EnergyTtw = 30,
                EnergyWtw = 50
            }
        };

        var artifactView1 = await client.ApiArtifactsPostAsync(new ArtifactDesc()
        {
            Title = "CO2 Emissions Report",
            Description = "Produced by CO2Path",
            Value = Newtonsoft.Json.JsonConvert.SerializeObject(report),
            AutomatedDownload = true
        });

        Console.WriteLine("Post Artifact " + artifactView1.Dump());
        Console.WriteLine("------");

        //var artifactView2 = await client.ApiArtifactsPostAsync(new ArtifactDesc()
        //{
        //    Title = "CO2 Emissions Report 2",
        //    Description = "Produced by CO2Path",
        //    Value = Newtonsoft.Json.JsonConvert.SerializeObject(report),
        //    AutomatedDownload = true
        //});

        var artifacts = new Uri[] {
            new Uri(artifactView1._links["self"].Href),
            // new Uri(artifactView2._links["self"].Href)
        };

        var artifacts2 = artifacts.Select(q => new Uri(q.AbsoluteUri.Replace("ids.co2path.io", "connectora"))).ToArray();

        //## Representations: POST /api/representations

        var r1 = await client.ApiRepresentationsPostAsync(new RepresentationDesc()
        {
            Title = "CO2 Emissions Report Representation",
            MediaType = "application/json",
            Language = "https://w3id.org/idsa/code/EN",
        });

        Console.WriteLine("Post Representations " + r1.Dump());
        Console.WriteLine("------");

        var representations = new Uri[] { new Uri(r1._links["self"].Href) };
        var representationId = Guid.Parse(r1._links["self"].Href[^36..]);

        //## Add Artifact to Representation: POST /api/representations/{id}/artifacts

        var addra = await client.ApiRepresentationsArtifactsPostAsync(representationId, artifacts);
        Console.WriteLine("Add Artifact to Representation " + addra.Dump());
        Console.WriteLine("------");

        //## Add Representation to Offer: POST /api/offers/{id}/representations

        var addro = await client.ApiOffersRepresentationsPostAsync(offersId, representations);
        Console.WriteLine("Add Representation to Offer " + addro.Dump());
        Console.WriteLine("------");

        //## Add Contract to Offer: POST /api/offers/{id}/contracts
        var add3 = await client.ApiOffersContractsPostAsync(offersId, contracts);
        Console.WriteLine("Add Contract to Offer " + add3.Dump());
        Console.WriteLine("------");

        var client2 = Client.CreateClient("https://ids.co2path.io:8081");

        //## Cheking successful registration
        var add4 = await client2.ApiIdsDescriptionAsyncEx(
            new Uri("https://broker-reverseproxy/infrastructure"), new Uri("https://ids.co2path.io/connectors/"));
        Console.WriteLine("Chek successful registration " + add4.Dump());
        Console.WriteLine("------");

        var Broker_catalog = add4.Graph[1].Id;

        //## Request Self-description from Connector B: POST /api/ids/description
        var a2 = await client2.ApiIdsDescriptionAsyncEx(new Uri("https://connectora:8080/api/ids/data"), null);
        Console.WriteLine("Request Self-description from Connector B " + a2.Dump());
        Console.WriteLine("------");

        var provider_catalog = a2.IdsResourceCatalog[0].Id;
        // like "https://connectora:8080/api/catalogs/b39eb545-6a46-49b0-b535-f9af069d6519"

        //## Request Information regarding the desired resource: POST /api/ids/description
        var a3 = await client2.ApiIdsDescriptionAsyncEx(new Uri("https://connectora:8080/api/ids/data"),
            new Uri(provider_catalog));
        Console.WriteLine("Request Information regarding the desired resource " + a3.Dump());
        Console.WriteLine("------");

        // var resource = a3.Id;

        //## Start Negotiation: POST /api/ids/contract

        var a5 = await client2.ApiIdsContractAsyncEx(
            new Uri("https://connectora:8080/api/ids/data"),
            offers2,
            artifacts2,
            false,
            new StartNegotiation[]  {
                new StartNegotiation() {
                Type="ids:Permission",
                    Id= rules[0].ToString(),
                    IdsDescription = new ValueTypeType[] { new ValueTypeType() {
                      Value= "Usage policy provide access applied",
                      Type= "http://www.w3.org/2001/XMLSchema#string"
                    } }  ,
                    IdsTitle = new ValueTypeType[]  { new ValueTypeType {
                       Value  = "Example Usage Policy",
                      Type= "http://www.w3.org/2001/XMLSchema#string"
                    } } ,
                    IdsAction= new IdType[] { new IdType {
                        Id= "https://w3id.org/idsa/code/USE"
                    } },

                    IdsTarget = artifacts[0].ToString()
                }
            });

        Console.WriteLine("Start Negotiation " + a5.Dump());
        Console.WriteLine("------");

        var agreementId = Guid.Parse(a5._links["self"].Href[^36..]);

        //## Request the Artifact based on the Existing Agreement: POST /api/agreements/{id}/artifacts

        var pages = await client2.ApiAgreementsArtifactsAsync(agreementId, null, null);
        Console.WriteLine("Request the Artifact based on the Existing Agreement: " + pages.Dump());
        Console.WriteLine("------");

        var dataLink = pages._embedded.Artifacts.ToArray()[0]._links["data"].Href;

        //await Task.Delay(5000);

        //## Obtain Data
        var data = await client2.HttpClient.GetStringAsync(dataLink);
        //var data = await client2.HttpClient.GetFromJsonAsync< RequestedResourceView>(dataLink);
        Console.WriteLine("Obtain Data: " + data.Dump());
        Console.WriteLine("------");


        Console.WriteLine("Offer "+ offers2[0].ToString());
        Console.WriteLine("Rules " + rules[0].ToString());
        Console.WriteLine("contractId " + contracts[0].ToString());
        Console.WriteLine("Artifacts " + artifacts2[0].ToString());
        Console.WriteLine("Aggreement "+agreementId);
        Console.WriteLine("representation " + representationId);
    }
}