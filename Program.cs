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
            NuviaRest.PreprocessRetryItems();
            NuviaRest.GetParameter("FacilityID");
            NuviaRest.SetParameter("spf", "Scott Farling");
            NuviaRest.SelectNextRecord();
            NuviaRest.Ccat_GetRecord();
        }


    }
}


