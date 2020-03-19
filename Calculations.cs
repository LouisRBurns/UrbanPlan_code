using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculations : MonoBehaviour {

    double ROI = 0; // Return on investment to developer
    double ROITarget = .135;
    double TotalProfit; 
    double TotalCost; 
    double MarketValue;
    double TotalSpace; // Total units or SF
    double TotalLowRise; // Variable to hold low rise absorption
    double TotalRetail; // Variable to hold retail absorption
    double AbsorptionLoss;
    double AbsorptionFactor;
    double DevCost; // Developer cost
    double ValuePerUnit;
    double ConstructionCost;
    double CityCost; // City subsidy on single building type
    double TotalCityCost;
    public Slider ROISlider;
    public Slider CitySlider;
    public Slider PHHRetailSlider;
    public Slider AffordableHousing;

    double Revenue = 0; // 10 yr value to the city
    double TaxBase;
    double AssessedValuePerUnit;
    double TaxRate;
    double HomelessShelterFee = 750000;
    bool HomelessShelterRelocate = true;
    
    /* Initialize building quantities and constants
    In this format:
    {   "REDPAM", // Residential Podium Apts Market Rate
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 225 }, // Total Absorption
                    { "unitsf", 20 }, // Units or SF/building
                    { "wt", 1 }, // Weight for oversupply
                    { "cost", 84000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 111000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 92130 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            }
    */
    public Dictionary<string, Dictionary<string, double>>
        BI =
        new Dictionary<string, Dictionary<string, double>>
        {
            {   "REDPAA", // Residential Podium Apts Affordable
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 450 }, // Total Absorption
                    { "unitsf", 20 }, // Units or SF/building
                    { "wt", 1 }, // Weight for oversupply
                    { "cost", 88200 }, // Cost per unit or SF
                    { "share", 0.1 }, // City share of cost
                    { "value", 35000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 29050 }, // tax value/unit or SF
                    { "tax", -2000 } // tax rate
                }
            },
            {   "REDPAM", // Residential Podium Apts Market Rate
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 225 }, // Total Absorption
                    { "unitsf", 20 }, // Units or SF/building
                    { "wt", 1 }, // Weight for oversupply
                    { "cost", 84000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 111000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 92130 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            },
            {   "REDTHA", // Residential Townhouses Affordable
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 300 }, // Total Absorption
                    { "unitsf", 6 }, // Units or SF/building
                    { "wt", 1 }, // Weight for oversupply
                    { "cost", 171150 }, // Cost per unit or SF
                    { "share", 0.1 }, // City share of cost
                    { "value", 68000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 56440 }, // tax value/unit or SF
                    { "tax", -2400 } // tax rate
                }
            },
            {   "REDTHM", // Residential Townhouses Market Rate
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 51 }, // Total Absorption
                    { "unitsf", 6 }, // Units or SF/building
                    { "wt", 1 }, // Weight for oversupply
                    { "cost", 163000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 212000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 175960 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            },
            {   "REDLXC", // Residential Luxury Condos
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 90 }, // Total Absorption
                    { "unitsf", 48 }, // Units or SF/building
                    { "wt", 2 }, // Weight for oversupply
                    { "cost", 192000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 270000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 224100 }, // tax value/unit or SF
                    { "tax", 0.0070 } // tax rate
                }
            },
            {   "REDPHS", // Phoenix Hotel Homeless Shelter
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 1 }, // Total Absorption
                    { "unitsf", 1 }, // Units or SF/building
                    { "wt", 0 }, // Weight for oversupply
                    { "cost", 0 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 1000 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.0050 } // tax rate
                }
            },
            {   "REDNHS", // New Homeless Shelter
                new Dictionary<string, double>
                {
                    // Same numbers as P. Hotel Homeless Shelter
                    { "qty", 0 }, // Quantity
                }
            },
            {   "OFFL1A", // Office Low-Rise 1A
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 238500 }, // Total Absorption
                    { "unitsf", 60000 }, // Units or SF/building
                    { "wt", 1.43 }, // Weight for oversupply
                    { "cost", 125 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 163.125 }, // Value per unit
                    { "jobs", 350 }, // SF per job created
                    { "assess", 135.4 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            },
            {   "OFFL1B", // Office Low-Rise 1B
                new Dictionary<string, double>
                {
                    // Same numbers as 1A
                    { "qty", 0 }, // Quantity
                }
            },
            {   "OFFLR2", // Office Low-Rise 2
                new Dictionary<string, double>
                {
                    // Same as other low-rise except:
                    { "qty", 0 }, // Quantity
                    { "unitsf", 80000 }, // Units or SF/building
                }
            },
            {   "OFFMID", // Office Mid-Rise
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 207000 }, // Total Absorption
                    { "unitsf", 120000 }, // Units or SF/building
                    { "wt", 1.25 }, // Weight for oversupply
                    { "cost", 185 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 242.35 }, // Value per unit
                    { "jobs", 350 }, // SF per job created
                    { "assess", 201 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            },
            {   "OFFPHH", // Office in Phoenix Hotel
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 60000 }, // Max SF/building
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 120 }, // Value per unit
                    { "jobs", 350 }, // SF per job created
                    { "assess", 99.6 }, // tax value/unit or SF
                    { "tax", 0.0075 } // tax rate
                }
            },
            {   "OFFYDG", // Office in York Dry Goods
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 48000 }, // Max SF/building
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 118 }, // Value per unit
                    { "jobs", 350 }, // SF per job created
                    { "assess", 97.94 }, // tax value/unit or SF
                    { "tax", 0.0065 } // tax rate
                }
            },
            {   "OFFVRW", // Office in Victorian Row
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 40000 }, // Max SF/building
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 118 }, // Value per unit
                    { "jobs", 350 }, // SF per job created
                    { "assess", 97.94 }, // tax value/unit or SF
                    { "tax", 0.0065 } // tax rate
                }
            },
            {   "RETNBH", // Retail Neighborhood
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 52500 }, // Total Absorption
                    { "unitsf", 5000 }, // Units or SF/building
                    { "wt", 1.43 }, // Weight for oversupply
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 130 }, // Value per unit
                    { "jobs", 300 }, // SF per job created
                    { "assess", 130 }, // tax value/unit or SF
                    { "tax", 0.020 } // tax rate
                }
            },
            {   "RETSPM", // Retail Supermarket
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 1 }, // Total Absorption
                    { "unitsf", 40000 }, // Units or SF/building
                    { "wt", 0 }, // Weight for oversupply
                    { "cost", 4600000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 149.5 }, // Value per unit
                    { "jobs", 200 }, // SF per job created
                    { "assess", 124.085 }, // tax value/unit or SF
                    { "tax", 0.010 } // tax rate
                }
            },
            {   "RETQMT", // Retail Q-Mart
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "abs", 1 }, // Total Absorption
                    { "unitsf", 80000 }, // Units or SF/building
                    { "wt", 0 }, // Weight for oversupply
                    { "cost", 10000000 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 162.5 }, // Value per unit
                    { "jobs", 200 }, // SF per job created
                    { "assess", 162.5 }, // tax value/unit or SF
                    { "tax", 0.0150 } // tax rate
                }
            },
            {   "RETPHH", // Retail Phoenix Hotel
                new Dictionary<string, double>
                {
                    // Counted with neighborhood retail
                    { "qty", 0 }, // Quantity
                    { "unitsf", 18000 }, // Max SF
                    { "cost", 100 },
                    { "share", 0 },
                    { "value", 130 },
                    { "rate", 1.3 },
                    { "jobs", 300 },
                    { "assess", 130 },
                    { "tax", 0.02}
                }
            },
            {   "RETYDG", // Retail York Dry Goods
                new Dictionary<string, double>
                {
                    // Counted with neighborhood retail
                    { "qty", 0 }, // Quantity
                    { "unitsf", 12000 }, // Max SF
                    { "value", 125 }, // Value per unit
                    { "jobs", 400 }, // SF per job created
                }
            },
            {   "RETVRW", // Retail Victorian Row
                new Dictionary<string, double>
                {
                    // Same as York Dry Goods
                    { "qty", 0 }, // Quantity
                    { "unitsf", 12000 }, // Max SF
                }
            },
            {   "YDGCOM", // York Dry Goods Community Facilities
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 60000 }, // Max SF/building
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0.15 }, // City share of cost
                    { "value", 70 }, // Value per unit
                    { "jobs", 700 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.01 } // tax rate
                }
            },
            {   "YDGCAS", // York Dry Goods Classes and Studio
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 60000 }, // Max SF/building
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 0 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.01 } // tax rate
                }
            },
            {   "VRWCAS", // Victorian Row Classes and Studio
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 25000 }, // Max SF for this use
                    { "cost", 100 }, // Cost per unit or SF
                    { "share", 0 }, // City share of cost
                    { "value", 0 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.01 } // tax rate
                }
            },
            {   "PRKPLZ", // Parks and Plazas
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 5000 }, // SF/building
                    { "cost", 37 }, // Cost per unit or SF
                    { "share", 0.50 }, // City share of cost
                    { "value", 0 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.027 } // tax rate
                }
            },
            {   "SPRTCT", // Sport courts
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 10000 }, // SF/building
                    { "cost", 39 }, // Cost per unit or SF
                    { "share", 0.50 }, // City share of cost
                    { "value", 0 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.025 } // tax rate
                }
            },
            {   "SKTPRK", // Skate park
                new Dictionary<string, double>
                {
                    { "qty", 0 }, // Quantity
                    { "unitsf", 10000 }, // SF/building
                    { "cost", 60 }, // Cost per unit or SF
                    { "share", 0.50 }, // City share of cost
                    { "value", 0 }, // Value per unit
                    { "jobs", 0 }, // SF per job created
                    { "assess", 0 }, // tax value/unit or SF
                    { "tax", 0.025 } // tax rate
                }
            },
        };

    List<string> LowRiseBuildings = new List<string>
    {
        "OFFL1A", "OFFL1B", "OFFLR2", "OFFPHH", "OFFYDG", "OFFVRW"
    };

    List<string> NeighborhoodRetail = new List<string>
    {
        "RETNBH", "RETPHH", "RETYDG", "RETVRW"
    };

    List<string> SingleUse = new List<string>
    {
        "REDPAA", "REDPAM", "REDTHA", "REDTHM", "REDLXC", "REDPHS",
        "OFFMID", "RETSPM", "RETQMT", "YDGCOM", "YDGCAS", "VRWCAS",
        "PRKPLZ", "SPRTCT", "SKTPRK"
    };

    List<string> SingleBuilding = new List<string>
    {
        "REDNHS", "OFFPHH", "OFFYDG", "OFFVRW", "RETSPM", "RETQMT", 
        "RETPHH", "RETYDG", "RETVRW", "YDGCOM", "YDGCAS", "VRWCAS"
    };

    List<string> Amenities = new List<string>
    {
        "PRKPLZ", "SPRTCT", "SKTPRK"
    };

	void Start () { // Called at the beginning

        UpdateMetrics(); // updates ROI and City calculations
	}
	
	void Update () { // Called once per frame
		
	}

    public void AddBuilding(string name)
    {
        Debug.Log(name + " qty: " + BI[name]["qty"]);
        if (SingleBuilding.Contains(name) && BI[name]["qty"] == 1)
            return;
        else
            BI[name]["qty"]++;

        Debug.Log(name + " qty: " + BI[name]["qty"]);
        UpdateMetrics();
    }

    public void RemoveBuilding(string name)
    {
        Debug.Log(name + " Before remove qty: " + BI[name]["qty"]);
        if (BI[name]["qty"] > 0)
        {
            BI[name]["qty"]--;
            Debug.Log(name + " After remove qty: " + BI[name]["qty"]);
            UpdateMetrics();
        }
    }

    public void UpdateMetrics()
    {
        ResetVariables();
        UpdateROI();
        UpdateCityRevenue();
    }

    public void ResetVariables()
    {
        // Reset absorption variables
        TotalLowRise = 0;
        TotalRetail = 0;
        TotalProfit = 0;
        TotalCost = 0;
    }

    
    public double UpdateROI()
    {
        foreach  (KeyValuePair<string, Dictionary<string, double>> b in BI)
        {
            if (b.Value["qty"] > 0)
            {
                TotalSpace = b.Value["qty"] * b.Value["unitsf"];
                Debug.Log("Total Space: " + TotalSpace);
                ConstructionCost = TotalSpace * b.Value["cost"];
                //Debug.Log("Construction costs: " + ConstructionCost);
                CityCost = 0;
                if ((b.Value.ContainsKey("share")) && b.Value["share"] != 0)
                    CityCost = b.Value["share"] * ConstructionCost;
                DevCost = ConstructionCost - CityCost + 7500000;
                //Debug.Log("Developer cost: " + DevCost);
                TotalCost += DevCost;
                Debug.Log("Total cost: " + TotalCost);

                MarketValue = TotalSpace * b.Value["value"];
                Debug.Log("Market Value: " + MarketValue);
                if (LowRiseBuildings.Contains(b.Key))
                    TotalLowRise += TotalSpace;
                Debug.Log("Total Low Rise: " + TotalLowRise);
                if (NeighborhoodRetail.Contains(b.Key))
                    TotalRetail += TotalSpace;
                if (SingleUse.Contains(b.Key))
                {
                    if ( b.Value["abs"] < TotalSpace )
                    {
                        AbsorptionFactor = (TotalSpace - b.Value["abs"]) /
                            (b.Value["abs"] / 3) * b.Value["wt"];
                        AbsorptionLoss = (MarketValue - DevCost) * 
                            (1 - AbsorptionFactor);    
                    }
                    TotalProfit += MarketValue - DevCost - AbsorptionLoss;
                }
                else
                    TotalProfit += MarketValue - DevCost;
                Debug.Log("Total Profit: " + TotalProfit);
            }
            
            // Subtract absorption loss from low rise and retail totals
            if (TotalLowRise > BI["OFFL1A"]["abs"])
            {
                AbsorptionFactor = (TotalLowRise - BI["OFFL1A"]["abs"]) / 
                    (BI["OFFL1A"]["abs"] / 3) * BI["OFFL1A"]["wt"];
                AbsorptionLoss = (TotalLowRise * (BI["OFFL1A"]["value"] -
                    BI["OFFL1A"]["cost"])) * AbsorptionFactor;
                TotalProfit -= AbsorptionLoss;
            }
            if (TotalRetail > BI["RETNBH"]["abs"])
            {
                AbsorptionFactor = (TotalRetail - BI["RETNBH"]["abs"]) / 
                    (BI["RETNBH"]["abs"] / 3) * BI["RETNBH"]["wt"];
                AbsorptionLoss = (TotalRetail * (BI["RETNBH"]["value"] -
                    BI["RETNBH"]["cost"])) * AbsorptionFactor;
                TotalProfit -= AbsorptionLoss;
            }
        }

        if (HomelessShelterRelocate)
            TotalProfit -= HomelessShelterFee;
        ROI = TotalProfit / TotalCost;
        Debug.Log("ROI: " + ROI);
        ROISlider.value = (float) (ROI / ROITarget);
        return ROI; // The ROI
    }
    
    public double UpdateCityRevenue()
    {
        // Caluculate City Costs
        foreach  (KeyValuePair<string, Dictionary<string, double>> b in BI)
        {
            if (b.Value["qty"] > 0)
            {
                TotalSpace = b.Value["qty"] * b.Value["unitsf"];
                ConstructionCost = TotalSpace * b.Value["cost"];

                if (!Amenities.Contains(b.Key) & (b.Key != "YDGCOM"))
                {
                    Revenue += TotalSpace * b.Value["assess"] * b.Value["tax"] * 10;
                    Debug.Log("Revenue: " + Revenue);
                }
                else if (Amenities.Contains(b.Key))
                    Revenue -= ConstructionCost * b.Value["tax"] * 10;
                else // lease payment for YDGCOM
                    Revenue -= TotalSpace * b.Value["value"] * 0.75;

                if ( (b.Value.ContainsKey("share")) && (b.Value["share"] > 0) )
                    CityCost += b.Value["share"] * ConstructionCost;
                    Revenue -= CityCost;
            }
        }
        if (HomelessShelterRelocate)
            Revenue += HomelessShelterFee;
        Revenue -= 10000000; // Cost of land
        CitySlider.value = (float) (Revenue / 1500000);
        return Revenue;
    }

    public void UpdateUnitSF()
    {
        BI["OFFPHH"]["unitsf"] = 60000 * (1 - PHHRetailSlider.value);
        BI["RETPHH"]["unitsf"] = 60000 - BI["OFFPHH"]["unitsf"];
        UpdateMetrics();
        Debug.Log("Office in PHH: " + BI["OFFPHH"]["unitsf"]);
        Debug.Log("Retail in PHH: " + BI["RETPHH"]["unitsf"]);
    }

}
