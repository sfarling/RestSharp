using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;


namespace RestSharpTest
{
    class Program
    {


        static void Main()
        {
            NuviaRest.GetParameter("Environment");
            NuviaRest.SetParameter("spf", "Scott Farling");
            NuviaRest.SelectNextRecord();
            NuviaRest.Ccat_GetRecord();
        }


    }
}


