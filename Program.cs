using System.Text.Json;

public class Covid
{
    public string satuanSuhu { get; set; }
    public int batas_hari_demam { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public Covid(string satuanSuhu, int batas_hari_demam, string pesan_ditolak, string pesan_diterima)
    {
        this.satuanSuhu = satuanSuhu;
        this.batas_hari_demam = batas_hari_demam;
        this.pesan_ditolak = pesan_ditolak;
        this.pesan_diterima = pesan_diterima;
    }
    public Covid() { }

}
public class CovidConfig
{
    public Covid config;
    public const string fileLocation = "C:\\Users\\ASUS\\OneDrive\\Documents\\Iqnaz\\KPL\\TPMOD8\\";
    public const string filePath = fileLocation + "config.json";

    public CovidConfig() 
    {
        try
        {
            readConfigFile();
        }
        catch
        {
            setDefault();
            writeCovidConfig();
        }
    }
    private Covid readConfigFile()
    {
        string configJsonData = File.ReadAllText(filePath);
        config = JsonSerializer.Deserialize<Covid>(configJsonData);
        return config;
    }

    public void setDefault()
    {
        string CONFIG1 = "celcius";
        int CONFIG2 = 14;
        string CONFIG3 = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        string CONFIG4 = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        config = new Covid(CONFIG1, CONFIG2, CONFIG3, CONFIG4);

    }

    private void writeCovidConfig()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        string jsonString = JsonSerializer.Serialize(config, options);
        File.WriteAllText(filePath, jsonString);
    }

    public void ubahSatuan()
    {
        if(config.satuanSuhu == "celcius")
        {
            config.satuanSuhu = "fahrenheit";
        }
        else
        {
            config.satuanSuhu = "celcius";
        }
    }
}


public class Program
{
    private static void Main(string[] args)
    {
        CovidConfig covidConfig = new CovidConfig();

        Console.WriteLine("Berapa suhu badan anda saat ini ? Dalam nilai " +  covidConfig.config.satuanSuhu);
        double suhuBadan = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int TerakhirDemam = Convert.ToInt16(Console.ReadLine());

        if (covidConfig.config.satuanSuhu == "celcius")
        {
            string pesan = suhuBadan >= 36.5 && suhuBadan <= 37.5 && TerakhirDemam < covidConfig.config.batas_hari_demam ? 
                covidConfig.config.pesan_diterima : covidConfig.config.pesan_ditolak;
            Console.WriteLine(pesan);
        }
        else if (covidConfig.config.satuanSuhu == "fahrenheit")
        {
            string pesan = suhuBadan >= 97.7 && suhuBadan <= 99.5 && TerakhirDemam < covidConfig.config.batas_hari_demam ? 
                covidConfig.config.pesan_diterima : covidConfig.config.pesan_ditolak;
            Console.WriteLine(pesan);
        }

        covidConfig.ubahSatuan();
        Console.WriteLine("Berapa suhu badan anda saat ini ? Dalam nilai " + covidConfig.config.satuanSuhu);
        suhuBadan = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        TerakhirDemam = Convert.ToInt16(Console.ReadLine());

        if (covidConfig.config.satuanSuhu == "celcius")
        {
            string pesan = suhuBadan >= 36.5 && suhuBadan <= 37.5 && TerakhirDemam < covidConfig.config.batas_hari_demam ? 
                covidConfig.config.pesan_diterima : covidConfig.config.pesan_ditolak;
            Console.WriteLine(pesan);
        }
        else if (covidConfig.config.satuanSuhu == "fahrenheit")
        {
            string pesan = suhuBadan >= 97.7 && suhuBadan <= 99.5 && TerakhirDemam < covidConfig.config.batas_hari_demam ? 
                covidConfig.config.pesan_diterima : covidConfig.config.pesan_ditolak;
            Console.WriteLine(pesan);
        }
    }
}