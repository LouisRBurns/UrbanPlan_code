# Improving Design Processes Through Interactive Geodesign
### Executive Summary
This was a combination of multiple projects I aggregated into my GISc Masters project. It included theory on gaming and simulation, public participation, visual representation, design and interaction, 3D modeling, level of detail, geographich ontology, and virtual reality. The main part of the presentation was a digital representation of the [UrbanPlan simulation](https://americas.uli.org/programs/urbanplan/).

### Files Included

* 20170811_Burns_GISc_Masters_Project_Presentation
* cacluations.cs - the script that replicates the UrbanPlan spreadsheet

### Data and Software

The data for this project came from the UrbanPlan spreadsheet and facilitator's manual, the City of Richardson (and their GIS department), and Dallas County GIS. Programs used included ESRI ArcGIS, ESRI ArcScene, Carto (formerly CartoDB), SketchUp, Unity, MeshLab, and C# (for Unity).

### Project Components

There were 3 main projects covering various levels of detail in interactive geodesign:
1. The digital rendering of the UrbanPlan simulation.
2. A virtual environment of Downtown Richardson.
3. A virtual environment of the Arts and Technology building as a Virtual Reality experience.

Each project is discussed in detail in the accompanying PowerPoint. 

### Discussion

One problem that I ran out of time (and later interest) to solve was that I did not realize that the models I created in SketchUp did not have triangulated faces and therefore were not detected in Unity. We were able to drag and drop objects created in Unity but not the ones created in SketchUp.

A second issue was that the ROI and city revenue bars still didn't give the best feedback. Until you finished filling up all the blocks, your actual amounts wouldn't be anywhere near the targets. That made it difficult to know how well you were doing as you went. It is still a huge improvement over the current method of waiting for the financial analyst to adjust 2-4 spreadsheet entries with each move.

The main reason I lost interest with the project and didn't go back and fix the objects was that I realized it wouldn't be feasible for me to simulate walkability with this framework. During the facilitations we would ask students to consider traffic patterns in their building placement but there was no way (or incentive) to attend to walkability or economic clustering. I needed to go beyond the financial spreadsheet.

### Results

The only feedback I got from faculty was that they would have liked to have seen more statistical analysis but that it was fine since I did programming. 

I have emailed ULI a couple of times to let them know I've completed the calculations for UrbanPlan but no one seems interested thus far. This was even after we were notified that ULI was attemping to pay a programmer to do a 2D digital site plan (without the calculations). In any event, it's here in case anyone changes their minds later.

This work could be used to further mixed reality experiences in the urban environment. It was also discussed further in my doctoral dissertation.