namespace Eco_Lib.RegHelper
{
    class RegistryHelper
    {
        private static RegistryHelper instance = null;
        public static RegistryHelper GetInstance()
        {
            return instance == null ? instance = new RegistryHelper() : instance;
        }
        private RegistryHelper()
        {

        }
    }
}
