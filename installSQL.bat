@ECHO OFF
echo Add DB QLMuaHang
sqlcmd -i scriptDB/ScriptDB.sql
echo Add trigger
sqlcmd -i scriptDB/trigger.sql
echo Add data type
sqlcmd -i scriptDB/type.sql
echo Add Data into QLMuaHang
sqlcmd -i scriptDB/data.sql
echo Add SP
sqlcmd -i scriptDB/SP.sql
echo Done
pause