# CashMachine
CashMachineRepository
Для запуска необходим .Net framework 4.5.2+
Локальный SQLServer, если не стоит .\SQLExpress, но есть SQLServer - то запускаем командную строку прописываем
SqlLocalDB.exe create MyInstance
затем запускаем инстанс SqlLocalDB.exe start MyInstance
в CashMachineApplication\App.config прописываем: 
connectionString="Data Source=(localDb)\MyInstance;
         Trusted_Connection=Yes;
         AttachDbFilename=путь к файлу БД\CashMachineTest.mdf"
         
         где путь к файлу БД - физическое расположение проекта\CashMachineApplication\AppData (к примеру:D:\Git\Project\CashMachineApplication\AppData\CashMachineTest.mdf)
         
         
         (localDb)\MyInstance - если нет .\SQLExpress, если есть то Data Source=.\SQLExpress
