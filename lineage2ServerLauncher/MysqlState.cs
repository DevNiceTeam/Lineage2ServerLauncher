namespace lineage2ServerLauncher
{
    class MysqlState
    {
        public bool isFirstRun; //Первый запуск
        public bool isLoading;  //Бд в процессе включения
        public bool isLoaded; //Бд включено и уже работает  
        public bool isReadyToLaunch; //Бд готова к запуску gs ls
        public bool isDisabled; //Бд выключена        
    }
}