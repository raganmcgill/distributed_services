Distributed services with Mass Transit playground

1. Download
2. Build\build.bat
3. cd \Demo
4. Load '0.Hospital.bat'
5. Load '1.DatabaseRegistry.bat'
6. Load '2.MonitorService.bat'
7. Follow instruction on Database Registry and post json (using Postman for example)

---------------------------------------------------------------------------------------------

0.Hospital.bat - Traffic light system to indicate if monitor is up

1.DatabaseRegistry.bat - Api for registration of 

2.MonitorService.bat - Montiors the schema change

3.DashboardRegistry.bat - Service to indicate drift

4.Notifications.bat - Basic windows notification when a database is registered

5.Protector.bat - Example of a protector service - when a database is registered or changed, it it holds sensitive data (first name, address, etc), Protector will flag it up

_FatalAttraction.bat - Kills all Wabbits and stores

_Full.bat - Loads all services up
