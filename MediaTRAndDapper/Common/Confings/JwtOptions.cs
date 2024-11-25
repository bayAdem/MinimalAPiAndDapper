namespace MediaTRAndDapper.Common.Confings
{
    public class JwtOptions : IConfig
    {
        public const string SECTION_NAME = "Jwt"; // yapılandırma (configuration) ayarlarını yönetmek için kullanılır.
        public JwtOptions()  // new'lemek için tanımladık.
        {

        }
        public string Key { get; set; }
        public string Issur { get; set; }
        public string Audience { get; set; }
        public int Duration { get; set; }
    }
}
