using CloudinaryDotNet;

namespace Eventures.Cloud
{
    public class ConnectToCloud
    {   
        public static  Account Account = new Account()
        {
            //TODO Hide link info
            Cloud = "duxtyuzpy",
            ApiKey = "749877646312376",
            ApiSecret = "112EsZQ5gMBLCOkWC3FvH2hJsGA"
        };
         public Cloudinary cloudinary = new Cloudinary(Account);
    }
}
