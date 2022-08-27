# AdventureSouls(Action Adventure Kit) integration for Scene Connector

This repository contains an example of how the Scene Connector asset can be used to expand the AdventureSouls demo that is part of the Action Adventure Kit asset.

- [Action Adventure Kit](https://adventure.softleitner.com/)
- [Scene Connector](https://sceneconnector.softleitner.com/)

## Setup

The packages are not included in this repository and have to be downloaded separately! After cloning the project will be missing the SoftLeitner folder.  
![project structure](https://github.com/Schossi/ConnectorSouls/blob/main/Project.PNG)  
You only need the SceneConnectorCore folder from SC for this integration demo. Download it from the [asset store](https://assetstore.unity.com/packages/templates/systems/action-adventure-kit-217284) or copy it from another project you have previously used it in if you want to avoid the errors when opening a project that is missing a lot of its files. You need both AdventureCore and AdventureSouls from AAK which you can also get on the [asset store](https://assetstore.unity.com/packages/templates/systems/action-adventure-kit-217284). Just like SC copying it over might be a bit less hassle as opening Unity before adding the assets would result in loads of errors.  

Once you have downloaded the two dependencies you start to try out the integration. Open up the ConnectorSouls.asset by double clicking it.  
![graph](https://github.com/Schossi/ConnectorSouls/blob/main/Graph.PNG)  
This scene connector graph contains all the scenes of the integration. The ConnectorSouls scene serves as a kind of entry point which can teleport you to the different demo scenes. You can double click it in the graph to open it and press play to explore the demos. Make sure to click SetBuild in the scene connector window once to add all the scenes to the build settings before playing.

## AB

The AB demo scenes serve as a minimum example of traversing between scenes. It is a good starting point for exploring how the integration works.  

Loading and Unloading is done with trigger areas that load the next area when entered and traverse into their scene when exited. The way the areas from A and B are overlapped makes it so the right scene is traversed into depending on where the player is.  

The Temp scenes that AdventureSouls uses are a bit special so SC does not support them out of the box. The integration provides the SoulsConnector class which inherits from SceneConnector and adds that needed bit of behavior of loading and unloading the Temp scene whenever the main one is loaded or unloaded.  

When the player moves from A to AB to B and traverses that would usually only unload AB as that is what the connector is attached to. To also unload the dangling A scene an event handler has been added in the UnloadingAsCounterpart event of the connector in AB.

## XY

The XY scenes demonstrate how SC may be used to work with ladders and elevators. It also contains simple examples of placeholders that can be shown when the actual scene is not currently loaded.

The connector that is used at the ladder is controlled by handler that have been added to the ladders events. When the action starts the other scene is loaded, if it ends at the top the connector is traversed and if the player goes back down instead it is unloaded again. Since both scenes contain a fully functional ladder the LoadedAsCounterpart event of the connector is used to deactivated the ladder of the newly added scene.  

The elevator in XY uses an entire scene that just contains the elevator. This works similarly to the AB demo scenes. The main difference is that the trigger areas don't overlap to decide which one should be traversed into. Instead it uses the special TraverseByHeight method the SoulsConnector provides which only traverses if the players height kind of matches up to the connectors. 