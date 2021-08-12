using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Configuration;

namespace RestSharpTest
{
    public class NuviaRest
    {

        //Build the REST client and POST
        public static string ClientConnectPost(string paramBody)
        {
            //Client
            IRestClient restClient = new RestSharp.RestClient(ConfigurationManager.AppSettings["NuviaExecuteProc"]);

            //Suppress Cert Errors
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            //Authentication
            restClient.Authenticator = new HttpBasicAuthenticator((ConfigurationManager.AppSettings["username"]), (ConfigurationManager.AppSettings["pwd"]));

            //POST
            IRestRequest restRequest = new RestRequest(Method.POST);

            //Build Header
            restRequest.AddHeader("Cache-Control", "no-cache");
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddParameter("application/json", paramBody, ParameterType.RequestBody);

            //Execute request to server
            IRestResponse restResponse = restClient.Execute(restRequest);

            //Extracting output data from received response
            string restResponseOutput = restResponse.Content;

            return restResponseOutput;
        }

        //Build the REST client and PATCH
        public static string ClientConnectPatch(string paramBody)
        {
            //Client
            IRestClient restClient = new RestSharp.RestClient(ConfigurationManager.AppSettings["NuviaProvideritems"]);

            //Suppress Cert Errors
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            //Authentication
            restClient.Authenticator = new HttpBasicAuthenticator((ConfigurationManager.AppSettings["username"]), (ConfigurationManager.AppSettings["pwd"]));

            //POST
            IRestRequest restRequest = new RestRequest(Method.POST);

            //Build Header
            restRequest.AddHeader("Cache-Control", "no-cache");
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddParameter("application/json", paramBody, ParameterType.RequestBody);

            //Execute request to server
            IRestResponse restResponse = restClient.Execute(restRequest);

            //Extracting output data from received response
            string restResponseOutput = restResponse.Content;

            return restResponseOutput;
        }

        //CplusParameter_GetValue(done)  CplusParameter_SetValue(done) Ccat_SelectNext(done) Ccat_UpdateStatus(done) Ccat_UpdateNote(done) 
        //Ccat_PreprocessRetryItems  

        /// <summary>
        /// Retrieves the value of a parameter stored in the database.
        /// <param name="paramName">Name of the parameter to retrieve.</param>
        /// <returns>Value of the given parameter if found; otherwise an empty string.</returns>
        public static string GetParameter(string paramName)
        {
            string value = "";

            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"CplusParameter_GetValue\"," +
                    "\"params\": [\"Name\"]," +
                    //"\"values\": [\"paramName\"]," +
                    "\"values\": [\"" + paramName + "\"]," +
                    "\"dataTypes\": [\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                //Deserialize the JSON results
                var myObj = JsonConvert.DeserializeObject<dynamic>(ClientConnectPost(paramBody));

                CplusParameter cplusparam = new CplusParameter();

                cplusparam.Value = myObj.recordsets[0].Value;
                value = cplusparam.Value;

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to retrieve parameter '" + paramName + "': " + ex.Message);
            }

            return value;
        }

        /// <summary>
        /// Updates the value of a parameter stored in the database.
        /// <param name="paramName">Name of the parameter to update.</param>
        /// <param name="paramValue">Value to assign to the parameter.</param>
        public static void SetParameter(string paramName, string paramValue)
        {
            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"CplusParameter_SetValue\"," +
                    "\"params\": [\"Name\",\"Value\"]," +
                    "\"values\": [\"" + paramName + "\",\"" + paramValue + "\"]," +
                    "\"dataTypes\": [\"String\",\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                ClientConnectPost(paramBody);

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to update parameter '" + paramName + "': " + ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the first CCAT record that is in the Pending state.
        /// Not all fields in the returned object will be populated. Only those needed by
        /// the application are populated.
        /// <returns>CcatItem object representing the CCAT record, or null if there are no
        /// records in the Pending state.  Also returns null if an error occurs.
        public static CcatRestItem SelectNextRecord()
        {
            CcatRestItem ccat = new CcatRestItem();

            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"Ccat_SelectNext\"," +
                    "\"params\": []," +                    //"\"values\": [\"paramName\"]," +
                    "\"values\": []," +
                    "\"dataTypes\": [\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                //Deserialize the JSON results
                var myObj = JsonConvert.DeserializeObject<dynamic>(ClientConnectPost(paramBody));

                ccat.ID = myObj.recordsets[0].ID;
                ccat.MmisUpdateStatus = myObj.recordsets[0].MmisUpdateStatus;
                ccat.MmisUpdateNote = myObj.recordsets[0].MmisUpdateNote;
                ccat.GHX_RECORD_ID = myObj.recordsets[0].GHX_RECORD_ID;
                ccat.GHX_RESITNUM = myObj.recordsets[0].GHX_RESITNUM;
                ccat.PROVIDER_ITEM_NUM = myObj.recordsets[0].PROVIDER_ITEM_NUM;
                ccat.ITEM_CREATE_DATE = myObj.recordsets[0].ITEM_CREATE_DATE;
                ccat.FACILITY_ID = myObj.recordsets[0].FACILITY_ID;
                ccat.GHX_ITEM_STATUS = myObj.recordsets[0].GHX_ITEM_STATUS;
                ccat.VENDOR_NAME = myObj.recordsets[0].VENDOR_NAME;
                ccat.GHX_VENDOR_NAME = myObj.recordsets[0].GHX_VENDOR_NAME;
                ccat.VENDOR_ID = myObj.recordsets[0].VENDOR_ID;
                ccat.VENDOR_PART_NUM = myObj.recordsets[0].VENDOR_PART_NUM;
                ccat.GHX_VEN_PART_NUM = myObj.recordsets[0].GHX_VEN_PART_NUM;
                ccat.NUV_APRV_VEN_PART_NUM = myObj.recordsets[0].NUV_APRV_VEN_PART_NUM;
                ccat.GHX_COR_VEN_PART_NUM = myObj.recordsets[0].GHX_COR_VEN_PART_NUM;
                ccat.GHX_SUB_PART_NUM = myObj.recordsets[0].GHX_SUB_PART_NUM;
                ccat.MFR_NAME = myObj.recordsets[0].MFR_NAME;
                ccat.GHX_MFR_NAME = myObj.recordsets[0].GHX_MFR_NAME;
                ccat.MFR_ID = myObj.recordsets[0].MFR_ID;
                ccat.MFR_PART_NUM = myObj.recordsets[0].MFR_PART_NUM;
                ccat.GHX_MFR_PART_NUM = myObj.recordsets[0].GHX_MFR_PART_NUM;
                ccat.NUV_APRV_MFR_PART_NUM = myObj.recordsets[0].NUV_APRV_MFR_PART_NUM;
                ccat.GHX_COR_MFR_PART_NUM = myObj.recordsets[0].GHX_COR_MFR_PART_NUM;
                ccat.PURCHASE_UOM = myObj.recordsets[0].PURCHASE_UOM;
                ccat.GHX_UOMALL = myObj.recordsets[0].GHX_UOMALL;
                ccat.GHX_UOM1 = myObj.recordsets[0].GHX_UOM1;
                ccat.GHX_UOM2 = myObj.recordsets[0].GHX_UOM2;
                ccat.GHX_UOM3 = myObj.recordsets[0].GHX_UOM3;
                ccat.GHX_UOM4 = myObj.recordsets[0].GHX_UOM4;
                ccat.GHX_UOM5 = myObj.recordsets[0].GHX_UOM5;
                ccat.PURCHASE_QOE = myObj.recordsets[0].PURCHASE_QOE;
                ccat.GHX_QOEALL = myObj.recordsets[0].GHX_QOEALL;
                ccat.GHX_QOE1 = myObj.recordsets[0].O;
                ccat.GHX_QOE2 = myObj.recordsets[0].GHX_QOE2;
                ccat.GHX_QOE3 = myObj.recordsets[0].GHX_QOE3;
                ccat.GHX_QOE4 = myObj.recordsets[0].GHX_QOE4;
                ccat.GHX_QOE5 = myObj.recordsets[0].GHX_QOE5;
                ccat.CHARGEABLE_FLAG = myObj.recordsets[0].CHARGEABLE_FLAG;
                ccat.TAXABLE_FLAG = myObj.recordsets[0].TAXABLE_FLAG;
                ccat.HAZARD_CODE = myObj.recordsets[0].HAZARD_CODE;
                ccat.LATEX_FLAG = myObj.recordsets[0].LATEX_FLAG;
                ccat.PART_DESC = myObj.recordsets[0].PART_DESC;
                ccat.PART_DESC2 = myObj.recordsets[0].PART_DESC2;
                ccat.PART_DESC3 = myObj.recordsets[0].PART_DESC3;
                ccat.GHXDESC_1 = myObj.recordsets[0].GHXDESC_1;
                ccat.GHXDESC_2 = myObj.recordsets[0].GHXDESC_2;
                ccat.GHXDESC_3 = myObj.recordsets[0].GHXDESC_3;
                ccat.GHXDESC_4 = myObj.recordsets[0].GHXDESC_4;
                ccat.GHXDESC_5 = myObj.recordsets[0].GHXDESC_5;
                ccat.GHXDESC_6 = myObj.recordsets[0].GHXDESC_6;
                ccat.GHXDESC_7 = myObj.recordsets[0].GHXDESC_7;
                ccat.GHXDESC_8 = myObj.recordsets[0].GHXDESC_8;
                ccat.GHXDESC_9 = myObj.recordsets[0].GHXDESC_9;
                ccat.GHXDESC_10 = myObj.recordsets[0].GHXDESC_10;
                ccat.GHXDESC_FULL = myObj.recordsets[0].GHXDESC_FULL;
                ccat.NUV_APRV_DESCRIPTION = myObj.recordsets[0].NUV_APRV_DESCRIPTION;
                ccat.UNSPSC_CODE = myObj.recordsets[0].UNSPSC_CODE;
                ccat.GHX_UNSPSC_CODE = myObj.recordsets[0].GHX_UNSPSC_CODE;
                ccat.NUV_APRV_UNSPSC_CODE = myObj.recordsets[0].NUV_APRV_UNSPSC_CODE;
                ccat.GHX_UNSPSC_SEGMENT = myObj.recordsets[0].GHX_UNSPSC_SEGMENT;
                ccat.GHX_UNSPSC_FAMILY = myObj.recordsets[0].GHX_UNSPSC_FAMILY;
                ccat.GHX_UNSPSC_CLASS = myObj.recordsets[0].GHX_UNSPSC_CLASS;
                ccat.GHX_UNSPSC_COMMODITY = myObj.recordsets[0].GHX_UNSPSC_COMMODITY;
                ccat.GHX_ATTR_NOUN = myObj.recordsets[0].GHX_ATTR_NOUN;
                ccat.GHX_ATTR_TYPE = myObj.recordsets[0].GHX_ATTR_TYPE;
                ccat.GHX_ATTR_BRAND_NAME = myObj.recordsets[0].GHX_ATTR_BRAND_NAME;
                ccat.GHX_ATTR_COMPOSITION = myObj.recordsets[0].GHX_ATTR_COMPOSITION;
                ccat.GHX_ATTR_AGE = myObj.recordsets[0].GHX_ATTR_AGE;
                ccat.GHX_ATTR_GENDER = myObj.recordsets[0].GHX_ATTR_GENDER;
                ccat.GHX_ATTR_SIZE = myObj.recordsets[0].GHX_ATTR_SIZE;
                ccat.GHX_ATTR_PRIMARY_LWH = myObj.recordsets[0].GHX_ATTR_PRIMARY_LWH;
                ccat.GHX_ATTR_SECOND_LWH = myObj.recordsets[0].GHX_ATTR_SECOND_LWH;
                ccat.GHX_ATTR_DIAMETER = myObj.recordsets[0].GHX_ATTR_DIAMETER;
                ccat.GHX_ATTR_VOLUME = myObj.recordsets[0].GHX_ATTR_VOLUME;
                ccat.GHX_ATTR_WEIGHT = myObj.recordsets[0].GHX_ATTR_WEIGHT;
                ccat.GHX_ATTR_LOCATION = myObj.recordsets[0].GHX_ATTR_LOCATION;
                ccat.GHX_ATTR_PROPERTIES = myObj.recordsets[0].GHX_ATTR_PROPERTIES;
                ccat.GHX_ATTR_STERILITY = myObj.recordsets[0].GHX_ATTR_STERILITY;
                ccat.GHX_ATTR_LATEX = myObj.recordsets[0].GHX_ATTR_LATEX;
                ccat.GHX_ATTR_DISPOSABLE = myObj.recordsets[0].GHX_ATTR_DISPOSABLE;
                ccat.GHX_ATTR_COLOR = myObj.recordsets[0].GHX_ATTR_COLOR;
                ccat.GHX_ATTR_FLAVOR = myObj.recordsets[0].GHX_ATTR_FLAVOR;
                ccat.GHX_ATTR_FRAGRANCE = myObj.recordsets[0].GHX_ATTR_FRAGRANCE;
                ccat.GHX_ATTR_HAZARDOUS = myObj.recordsets[0].GHX_ATTR_HAZARDOUS;
                ccat.GHX_ATTR_OTHER = myObj.recordsets[0].GHX_ATTR_OTHER;
                ccat.MISC1 = myObj.recordsets[0].MISC1;
                ccat.MISC2 = myObj.recordsets[0].MISC2;
                ccat.MISC3 = myObj.recordsets[0].MISC3;
                ccat.MISC4 = myObj.recordsets[0].MISC4;
                ccat.MISC5 = myObj.recordsets[0].MISC5;
                ccat.MISC6 = myObj.recordsets[0].MISC6;
                ccat.MISC7 = myObj.recordsets[0].MISC7;
                ccat.MISC8 = myObj.recordsets[0].MISC8;
                ccat.MISC9 = myObj.recordsets[0].MISC9;
                ccat.MISC10 = myObj.recordsets[0].MISC10;
                ccat.UNIT_PRICE = myObj.recordsets[0].UNIT_PRICE;
                ccat.CONTRACT_NUM = myObj.recordsets[0].CONTRACT_NUM;
                ccat.ITEM_CLASSIFICATION_CODE = myObj.recordsets[0].ITEM_CLASSIFICATION_CODE;
                ccat.VENDOR_SEQUENCE = myObj.recordsets[0].VENDOR_SEQUENCE;
                ccat.GHX_HCPCS_CODE = myObj.recordsets[0].GHX_HCPCS_CODE;
                ccat.NUV_APRV_HCPCS_CODE = myObj.recordsets[0].NUV_APRV_HCPCS_CODE;
                ccat.GHX_HCPCS_DESC = myObj.recordsets[0].GHX_HCPCS_DESC;
                ccat.GHX_HCPCS_STATUS = myObj.recordsets[0].GHX_HCPCS_STATUS;
                ccat.GHX_DUP_BY_VEN = myObj.recordsets[0].GHX_DUP_BY_VEN;
                ccat.GHX_DUP_BY_MFR = myObj.recordsets[0].GHX_DUP_BY_MFR;
                ccat.GHX_DUP_ITEM_FLG = myObj.recordsets[0].GHX_DUP_ITEM_FLG;
                ccat.GHX_INCLUDE_CODE = myObj.recordsets[0].GHX_INCLUDE_CODE;
                ccat.GHX_EXCLUDE_REASON = myObj.recordsets[0].GHX_EXCLUDE_REASON;
                ccat.ORIG_DESC_DUPID = myObj.recordsets[0].ORIG_DESC_DUPID;
                ccat.GHX_ACTION = myObj.recordsets[0].GHX_ACTION;
                ccat.PACKAGING_STRING = myObj.recordsets[0].PACKAGING_STRING;
                ccat.BASE_PRICE = myObj.recordsets[0].BASE_PRICE;
                ccat.MARKUP = myObj.recordsets[0].MARKUP;
                ccat.IMPLANTABLE_FLAG = myObj.recordsets[0].IMPLANTABLE_FLAG;
                ccat.REUSABLE_FLAG = myObj.recordsets[0].REUSABLE_FLAG;
                ccat.CONSIGNMENT_FLAG = myObj.recordsets[0].CONSIGNMENT_FLAG;
                ccat.HCPCS_CODE = myObj.recordsets[0].HCPCS_CODE;
                ccat.ITEM_STATUS = myObj.recordsets[0].ITEM_STATUS;
                ccat.ITEM_UPDATE_DATE = myObj.recordsets[0].ITEM_UPDATE_DATE;
                ccat.GL_CODE = myObj.recordsets[0].GL_CODE;
                ccat.OR_PREF = myObj.recordsets[0].OR_PREF;
                ccat.CHARGE_CODE = myObj.recordsets[0].CHARGE_CODE;
                ccat.REVENUE_CODE = myObj.recordsets[0].REVENUE_CODE;
                ccat.STOCKED_FLAG = myObj.recordsets[0].STOCKED_FLAG;
                ccat.GHX_CONT_ID1 = myObj.recordsets[0].GHX_CONT_ID1;
                ccat.GHX_CONT_DESC1 = myObj.recordsets[0].GHX_CONT_DESC1;
                ccat.GHX_CONT_EFF_DATE1 = myObj.recordsets[0].GHX_CONT_EFF_DATE1;
                ccat.GHX_CONT_EXP_DATE1 = myObj.recordsets[0].GHX_CONT_EXP_DATE1;
                ccat.GHX_CONT_TIER1 = myObj.recordsets[0].GHX_CONT_TIER1;
                ccat.GHX_CONT_UOM1 = myObj.recordsets[0].GHX_CONT_UOM1;
                ccat.GHX_CONT_QOE1 = myObj.recordsets[0].GHX_CONT_QOE1;
                ccat.GHX_CONT_PRICE1 = myObj.recordsets[0].GHX_CONT_PRICE1;
                ccat.GHX_CONT_TYPE1 = myObj.recordsets[0].GHX_CONT_TYPE1;
                ccat.GHX_CONT_ID2 = myObj.recordsets[0].GHX_CONT_ID2;
                ccat.GHX_CONT_DESC2 = myObj.recordsets[0].GHX_CONT_DESC2;
                ccat.GHX_CONT_EFF_DATE2 = myObj.recordsets[0].GHX_CONT_EFF_DATE2;
                ccat.GHX_CONT_EXP_DATE2 = myObj.recordsets[0].GHX_CONT_EXP_DATE2;
                ccat.GHX_CONT_TIER2 = myObj.recordsets[0].GHX_CONT_TIER2;
                ccat.GHX_CONT_UOM2 = myObj.recordsets[0].GHX_CONT_UOM2;
                ccat.GHX_CONT_QOE2 = myObj.recordsets[0].GHX_CONT_QOE2;
                ccat.GHX_CONT_PRICE2 = myObj.recordsets[0].GHX_CONT_PRICE2;
                ccat.GHX_CONT_TYPE2 = myObj.recordsets[0].GHX_CONT_TYPE2;
                ccat.GHX_CONT_ID3 = myObj.recordsets[0].GHX_CONT_ID3;
                ccat.GHX_CONT_DESC3 = myObj.recordsets[0].GHX_CONT_DESC3;
                ccat.GHX_CONT_EFF_DATE3 = myObj.recordsets[0].GHX_CONT_EFF_DATE3;
                ccat.GHX_CONT_EXP_DATE3 = myObj.recordsets[0].GHX_CONT_EXP_DATE3;
                ccat.GHX_CONT_TIER3 = myObj.recordsets[0].GHX_CONT_TIER3;
                ccat.GHX_CONT_UOM3 = myObj.recordsets[0].GHX_CONT_UOM3;
                ccat.GHX_CONT_QOE3 = myObj.recordsets[0].GHX_CONT_QOE3;
                ccat.GHX_CONT_PRICE3 = myObj.recordsets[0].GHX_CONT_PRICE3;
                ccat.GHX_CONT_TYPE3 = myObj.recordsets[0].GHX_CONT_TYPE3;
                ccat.GHX_CONT_ID4 = myObj.recordsets[0].GHX_CONT_ID4;
                ccat.GHX_CONT_DESC4 = myObj.recordsets[0].GHX_CONT_DESC4;
                ccat.GHX_CONT_EFF_DATE4 = myObj.recordsets[0].GHX_CONT_EFF_DATE4;
                ccat.GHX_CONT_EXP_DATE4 = myObj.recordsets[0].GHX_CONT_EXP_DATE4;
                ccat.GHX_CONT_TIER4 = myObj.recordsets[0].GHX_CONT_TIER4;
                ccat.GHX_CONT_UOM4 = myObj.recordsets[0].GHX_CONT_UOM4;
                ccat.GHX_CONT_QOE4 = myObj.recordsets[0].GHX_CONT_QOE4;
                ccat.GHX_CONT_PRICE4 = myObj.recordsets[0].GHX_CONT_PRICE4;
                ccat.GHX_CONT_TYPE4 = myObj.recordsets[0].GHX_CONT_TYPE4;
                ccat.GHX_CONT_ID5 = myObj.recordsets[0].GHX_CONT_ID5;
                ccat.GHX_CONT_DESC5 = myObj.recordsets[0].GHX_CONT_DESC5;
                ccat.GHX_CONT_EFF_DATE5 = myObj.recordsets[0].GHX_CONT_EFF_DATE5;
                ccat.GHX_CONT_EXP_DATE5 = myObj.recordsets[0].GHX_CONT_EXP_DATE5;
                ccat.GHX_CONT_TIER5 = myObj.recordsets[0].GHX_CONT_TIER5;
                ccat.GHX_CONT_UOM5 = myObj.recordsets[0].GHX_CONT_UOM5;
                ccat.GHX_CONT_QOE5 = myObj.recordsets[0].GHX_CONT_QOE5;
                ccat.GHX_CONT_PRICE5 = myObj.recordsets[0].GHX_CONT_PRICE5;
                ccat.GHX_CONT_TYPE5 = myObj.recordsets[0].GHX_CONT_TYPE5;
                ccat.NUV_APRV_CONT_PRICE = myObj.recordsets[0].NUV_APRV_CONT_PRICE;
                ccat.GUDID_PRIMARY_DI = myObj.recordsets[0].GUDID_PRIMARY_DI;

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to retrieve CCAT item record: " + ex.Message);
            }

            return ccat;
        }

        /// <summary>
        /// Updates the status of an item.
        /// </summary>
        /// <param name="ccat">Object representing the item to update.</param>
        /// <param name="status">New status value.</param>
        public static void UpdateItemStatus(CcatRestItem ccat)
        {
            try
            {
                string statusText = "";
                switch (ccat.NewStatus)
                {
                    case NuviaItemStatus.Complete: statusText = "Complete"; break;
                    case NuviaItemStatus.Pending: statusText = "Pending"; break;
                    case NuviaItemStatus.Retry: statusText = "Retry"; break;
                    case NuviaItemStatus.MeditechError: statusText = "MeditechError"; break;
                    case NuviaItemStatus.ScriptError: statusText = "ScriptError"; break;
                    case NuviaItemStatus.Ignore: statusText = "Ignore"; break;
                }

                //add Body parameter to REST request
                var paramBody = "{\"name\": \"Ccat_UpdateStatus\"," +
                    "\"params\": [\"ID\",\"MmisUpdateStatus\"]," +
                    "\"values\": [\"" + ccat.ID + "\",\"" + statusText + "\"]," +
                    "\"dataTypes\": [\"Int\",\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                ClientConnectPost(paramBody);

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to update CCAT item status: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the note for an item.
        /// </summary>
        /// <param name="ccat">Object representing the item to update.</param>
        /// <param name="note">Text of the new note.</param>
        public static void UpdateItemNote(CcatRestItem ccat)
        {
            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"Ccat_UpdateNote\"," +
                    "\"params\": [\"ID\",\"DetailStatusMessage\"]," +
                    //"\"values\": [\"" + ccat.ID + "\",\"" + ccat.DetailStatusMessage + "\"]," +
                    "\"values\": [\"" + ccat.ID + "\",\"" + ccat.DetailStatusMessage + "\"]," +
                    "\"dataTypes\": [\"Int\",\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                ClientConnectPost(paramBody);

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to update CCAT item status: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the provider item number value for the given record.
        /// </summary>
        /// <param name="record">The CCAT record to update.</param>
        public static void UpdateProviderItemNumber(CcatRestItem ccat)
        {

            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"providerId\":\"" + ccat.ID + "\",\"" +
                    "\"itemNumber\":\"" + ccat.PROVIDER_ITEM_NUM + "\",\"" +
                    "\"schemaName\": \"Nuvia\"}";

                ClientConnectPatch(paramBody);

                //At this point, the database has the new provider item number, so all future queries need
                // that value set.
                ccat.PROVIDER_ITEM_NUM = ccat.NewProviderItemNumber;
            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to update CCAT with new item number: " + ex.Message);

                // By rethrowing this, we will bubble up and be able to mark the record as failed so someone will know
                // to look at it manually.
                throw;
            }
        }

        public static void Ccat_GetRecord()
        {
            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"Ccat_GetRecord\"," +
                    "\"params\": [\"PROVIDER_ITEM_NUM\",\"FACILITY_ID\",\"VENDOR_ID\"]," +
                    "\"values\": [\"ITM001057\",\"SEH\",\"55\"]," +
                    "\"dataTypes\": [\"String\",\"String\",\"String\"]," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                //Deserialize the JSON results
                var myObj = JsonConvert.DeserializeObject<dynamic>(ClientConnectPost(paramBody));

                CcatRestItem ccat = new CcatRestItem();

                ccat.ID = myObj.recordsets[0].ID;
                ccat.MmisUpdateStatus = myObj.recordsets[0].MmisUpdateStatus;
                ccat.MmisUpdateNote = myObj.recordsets[0].MmisUpdateNote;
                ccat.GHX_RECORD_ID = myObj.recordsets[0].GHX_RECORD_ID;
                ccat.GHX_RESITNUM = myObj.recordsets[0].GHX_RESITNUM;
                ccat.PROVIDER_ITEM_NUM = myObj.recordsets[0].PROVIDER_ITEM_NUM;
                ccat.ITEM_CREATE_DATE = myObj.recordsets[0].ITEM_CREATE_DATE;
                ccat.FACILITY_ID = myObj.recordsets[0].FACILITY_ID;
                ccat.GHX_ITEM_STATUS = myObj.recordsets[0].GHX_ITEM_STATUS;
                ccat.VENDOR_NAME = myObj.recordsets[0].VENDOR_NAME;
                ccat.GHX_VENDOR_NAME = myObj.recordsets[0].GHX_VENDOR_NAME;
                ccat.VENDOR_ID = myObj.recordsets[0].VENDOR_ID;
                ccat.VENDOR_PART_NUM = myObj.recordsets[0].VENDOR_PART_NUM;
                ccat.GHX_VEN_PART_NUM = myObj.recordsets[0].GHX_VEN_PART_NUM;
                ccat.NUV_APRV_VEN_PART_NUM = myObj.recordsets[0].NUV_APRV_VEN_PART_NUM;
                ccat.GHX_COR_VEN_PART_NUM = myObj.recordsets[0].GHX_COR_VEN_PART_NUM;
                ccat.GHX_SUB_PART_NUM = myObj.recordsets[0].GHX_SUB_PART_NUM;
                ccat.MFR_NAME = myObj.recordsets[0].MFR_NAME;
                ccat.GHX_MFR_NAME = myObj.recordsets[0].GHX_MFR_NAME;
                ccat.MFR_ID = myObj.recordsets[0].MFR_ID;
                ccat.MFR_PART_NUM = myObj.recordsets[0].MFR_PART_NUM;
                ccat.GHX_MFR_PART_NUM = myObj.recordsets[0].GHX_MFR_PART_NUM;
                ccat.NUV_APRV_MFR_PART_NUM = myObj.recordsets[0].NUV_APRV_MFR_PART_NUM;
                ccat.GHX_COR_MFR_PART_NUM = myObj.recordsets[0].GHX_COR_MFR_PART_NUM;
                ccat.PURCHASE_UOM = myObj.recordsets[0].PURCHASE_UOM;
                ccat.GHX_UOMALL = myObj.recordsets[0].GHX_UOMALL;
                ccat.GHX_UOM1 = myObj.recordsets[0].GHX_UOM1;
                ccat.GHX_UOM2 = myObj.recordsets[0].GHX_UOM2;
                ccat.GHX_UOM3 = myObj.recordsets[0].GHX_UOM3;
                ccat.GHX_UOM4 = myObj.recordsets[0].GHX_UOM4;
                ccat.GHX_UOM5 = myObj.recordsets[0].GHX_UOM5;
                ccat.PURCHASE_QOE = myObj.recordsets[0].PURCHASE_QOE;
                ccat.GHX_QOEALL = myObj.recordsets[0].GHX_QOEALL;
                ccat.GHX_QOE1 = myObj.recordsets[0].O;
                ccat.GHX_QOE2 = myObj.recordsets[0].GHX_QOE2;
                ccat.GHX_QOE3 = myObj.recordsets[0].GHX_QOE3;
                ccat.GHX_QOE4 = myObj.recordsets[0].GHX_QOE4;
                ccat.GHX_QOE5 = myObj.recordsets[0].GHX_QOE5;
                ccat.CHARGEABLE_FLAG = myObj.recordsets[0].CHARGEABLE_FLAG;
                ccat.TAXABLE_FLAG = myObj.recordsets[0].TAXABLE_FLAG;
                ccat.HAZARD_CODE = myObj.recordsets[0].HAZARD_CODE;
                ccat.LATEX_FLAG = myObj.recordsets[0].LATEX_FLAG;
                ccat.PART_DESC = myObj.recordsets[0].PART_DESC;
                ccat.PART_DESC2 = myObj.recordsets[0].PART_DESC2;
                ccat.PART_DESC3 = myObj.recordsets[0].PART_DESC3;
                ccat.GHXDESC_1 = myObj.recordsets[0].GHXDESC_1;
                ccat.GHXDESC_2 = myObj.recordsets[0].GHXDESC_2;
                ccat.GHXDESC_3 = myObj.recordsets[0].GHXDESC_3;
                ccat.GHXDESC_4 = myObj.recordsets[0].GHXDESC_4;
                ccat.GHXDESC_5 = myObj.recordsets[0].GHXDESC_5;
                ccat.GHXDESC_6 = myObj.recordsets[0].GHXDESC_6;
                ccat.GHXDESC_7 = myObj.recordsets[0].GHXDESC_7;
                ccat.GHXDESC_8 = myObj.recordsets[0].GHXDESC_8;
                ccat.GHXDESC_9 = myObj.recordsets[0].GHXDESC_9;
                ccat.GHXDESC_10 = myObj.recordsets[0].GHXDESC_10;
                ccat.GHXDESC_FULL = myObj.recordsets[0].GHXDESC_FULL;
                ccat.NUV_APRV_DESCRIPTION = myObj.recordsets[0].NUV_APRV_DESCRIPTION;
                ccat.UNSPSC_CODE = myObj.recordsets[0].UNSPSC_CODE;
                ccat.GHX_UNSPSC_CODE = myObj.recordsets[0].GHX_UNSPSC_CODE;
                ccat.NUV_APRV_UNSPSC_CODE = myObj.recordsets[0].NUV_APRV_UNSPSC_CODE;
                ccat.GHX_UNSPSC_SEGMENT = myObj.recordsets[0].GHX_UNSPSC_SEGMENT;
                ccat.GHX_UNSPSC_FAMILY = myObj.recordsets[0].GHX_UNSPSC_FAMILY;
                ccat.GHX_UNSPSC_CLASS = myObj.recordsets[0].GHX_UNSPSC_CLASS;
                ccat.GHX_UNSPSC_COMMODITY = myObj.recordsets[0].GHX_UNSPSC_COMMODITY;
                ccat.GHX_ATTR_NOUN = myObj.recordsets[0].GHX_ATTR_NOUN;
                ccat.GHX_ATTR_TYPE = myObj.recordsets[0].GHX_ATTR_TYPE;
                ccat.GHX_ATTR_BRAND_NAME = myObj.recordsets[0].GHX_ATTR_BRAND_NAME;
                ccat.GHX_ATTR_COMPOSITION = myObj.recordsets[0].GHX_ATTR_COMPOSITION;
                ccat.GHX_ATTR_AGE = myObj.recordsets[0].GHX_ATTR_AGE;
                ccat.GHX_ATTR_GENDER = myObj.recordsets[0].GHX_ATTR_GENDER;
                ccat.GHX_ATTR_SIZE = myObj.recordsets[0].GHX_ATTR_SIZE;
                ccat.GHX_ATTR_PRIMARY_LWH = myObj.recordsets[0].GHX_ATTR_PRIMARY_LWH;
                ccat.GHX_ATTR_SECOND_LWH = myObj.recordsets[0].GHX_ATTR_SECOND_LWH;
                ccat.GHX_ATTR_DIAMETER = myObj.recordsets[0].GHX_ATTR_DIAMETER;
                ccat.GHX_ATTR_VOLUME = myObj.recordsets[0].GHX_ATTR_VOLUME;
                ccat.GHX_ATTR_WEIGHT = myObj.recordsets[0].GHX_ATTR_WEIGHT;
                ccat.GHX_ATTR_LOCATION = myObj.recordsets[0].GHX_ATTR_LOCATION;
                ccat.GHX_ATTR_PROPERTIES = myObj.recordsets[0].GHX_ATTR_PROPERTIES;
                ccat.GHX_ATTR_STERILITY = myObj.recordsets[0].GHX_ATTR_STERILITY;
                ccat.GHX_ATTR_LATEX = myObj.recordsets[0].GHX_ATTR_LATEX;
                ccat.GHX_ATTR_DISPOSABLE = myObj.recordsets[0].GHX_ATTR_DISPOSABLE;
                ccat.GHX_ATTR_COLOR = myObj.recordsets[0].GHX_ATTR_COLOR;
                ccat.GHX_ATTR_FLAVOR = myObj.recordsets[0].GHX_ATTR_FLAVOR;
                ccat.GHX_ATTR_FRAGRANCE = myObj.recordsets[0].GHX_ATTR_FRAGRANCE;
                ccat.GHX_ATTR_HAZARDOUS = myObj.recordsets[0].GHX_ATTR_HAZARDOUS;
                ccat.GHX_ATTR_OTHER = myObj.recordsets[0].GHX_ATTR_OTHER;
                ccat.MISC1 = myObj.recordsets[0].MISC1;
                ccat.MISC2 = myObj.recordsets[0].MISC2;
                ccat.MISC3 = myObj.recordsets[0].MISC3;
                ccat.MISC4 = myObj.recordsets[0].MISC4;
                ccat.MISC5 = myObj.recordsets[0].MISC5;
                ccat.MISC6 = myObj.recordsets[0].MISC6;
                ccat.MISC7 = myObj.recordsets[0].MISC7;
                ccat.MISC8 = myObj.recordsets[0].MISC8;
                ccat.MISC9 = myObj.recordsets[0].MISC9;
                ccat.MISC10 = myObj.recordsets[0].MISC10;
                ccat.UNIT_PRICE = myObj.recordsets[0].UNIT_PRICE;
                ccat.CONTRACT_NUM = myObj.recordsets[0].CONTRACT_NUM;
                ccat.ITEM_CLASSIFICATION_CODE = myObj.recordsets[0].ITEM_CLASSIFICATION_CODE;
                ccat.VENDOR_SEQUENCE = myObj.recordsets[0].VENDOR_SEQUENCE;
                ccat.GHX_HCPCS_CODE = myObj.recordsets[0].GHX_HCPCS_CODE;
                ccat.NUV_APRV_HCPCS_CODE = myObj.recordsets[0].NUV_APRV_HCPCS_CODE;
                ccat.GHX_HCPCS_DESC = myObj.recordsets[0].GHX_HCPCS_DESC;
                ccat.GHX_HCPCS_STATUS = myObj.recordsets[0].GHX_HCPCS_STATUS;
                ccat.GHX_DUP_BY_VEN = myObj.recordsets[0].GHX_DUP_BY_VEN;
                ccat.GHX_DUP_BY_MFR = myObj.recordsets[0].GHX_DUP_BY_MFR;
                ccat.GHX_DUP_ITEM_FLG = myObj.recordsets[0].GHX_DUP_ITEM_FLG;
                ccat.GHX_INCLUDE_CODE = myObj.recordsets[0].GHX_INCLUDE_CODE;
                ccat.GHX_EXCLUDE_REASON = myObj.recordsets[0].GHX_EXCLUDE_REASON;
                ccat.ORIG_DESC_DUPID = myObj.recordsets[0].ORIG_DESC_DUPID;
                ccat.GHX_ACTION = myObj.recordsets[0].GHX_ACTION;
                ccat.PACKAGING_STRING = myObj.recordsets[0].PACKAGING_STRING;
                ccat.BASE_PRICE = myObj.recordsets[0].BASE_PRICE;
                ccat.MARKUP = myObj.recordsets[0].MARKUP;
                ccat.IMPLANTABLE_FLAG = myObj.recordsets[0].IMPLANTABLE_FLAG;
                ccat.REUSABLE_FLAG = myObj.recordsets[0].REUSABLE_FLAG;
                ccat.CONSIGNMENT_FLAG = myObj.recordsets[0].CONSIGNMENT_FLAG;
                ccat.HCPCS_CODE = myObj.recordsets[0].HCPCS_CODE;
                ccat.ITEM_STATUS = myObj.recordsets[0].ITEM_STATUS;
                ccat.ITEM_UPDATE_DATE = myObj.recordsets[0].ITEM_UPDATE_DATE;
                ccat.GL_CODE = myObj.recordsets[0].GL_CODE;
                ccat.OR_PREF = myObj.recordsets[0].OR_PREF;
                ccat.CHARGE_CODE = myObj.recordsets[0].CHARGE_CODE;
                ccat.REVENUE_CODE = myObj.recordsets[0].REVENUE_CODE;
                ccat.STOCKED_FLAG = myObj.recordsets[0].STOCKED_FLAG;
                ccat.GHX_CONT_ID1 = myObj.recordsets[0].GHX_CONT_ID1;
                ccat.GHX_CONT_DESC1 = myObj.recordsets[0].GHX_CONT_DESC1;
                ccat.GHX_CONT_EFF_DATE1 = myObj.recordsets[0].GHX_CONT_EFF_DATE1;
                ccat.GHX_CONT_EXP_DATE1 = myObj.recordsets[0].GHX_CONT_EXP_DATE1;
                ccat.GHX_CONT_TIER1 = myObj.recordsets[0].GHX_CONT_TIER1;
                ccat.GHX_CONT_UOM1 = myObj.recordsets[0].GHX_CONT_UOM1;
                ccat.GHX_CONT_QOE1 = myObj.recordsets[0].GHX_CONT_QOE1;
                ccat.GHX_CONT_PRICE1 = myObj.recordsets[0].GHX_CONT_PRICE1;
                ccat.GHX_CONT_TYPE1 = myObj.recordsets[0].GHX_CONT_TYPE1;
                ccat.GHX_CONT_ID2 = myObj.recordsets[0].GHX_CONT_ID2;
                ccat.GHX_CONT_DESC2 = myObj.recordsets[0].GHX_CONT_DESC2;
                ccat.GHX_CONT_EFF_DATE2 = myObj.recordsets[0].GHX_CONT_EFF_DATE2;
                ccat.GHX_CONT_EXP_DATE2 = myObj.recordsets[0].GHX_CONT_EXP_DATE2;
                ccat.GHX_CONT_TIER2 = myObj.recordsets[0].GHX_CONT_TIER2;
                ccat.GHX_CONT_UOM2 = myObj.recordsets[0].GHX_CONT_UOM2;
                ccat.GHX_CONT_QOE2 = myObj.recordsets[0].GHX_CONT_QOE2;
                ccat.GHX_CONT_PRICE2 = myObj.recordsets[0].GHX_CONT_PRICE2;
                ccat.GHX_CONT_TYPE2 = myObj.recordsets[0].GHX_CONT_TYPE2;
                ccat.GHX_CONT_ID3 = myObj.recordsets[0].GHX_CONT_ID3;
                ccat.GHX_CONT_DESC3 = myObj.recordsets[0].GHX_CONT_DESC3;
                ccat.GHX_CONT_EFF_DATE3 = myObj.recordsets[0].GHX_CONT_EFF_DATE3;
                ccat.GHX_CONT_EXP_DATE3 = myObj.recordsets[0].GHX_CONT_EXP_DATE3;
                ccat.GHX_CONT_TIER3 = myObj.recordsets[0].GHX_CONT_TIER3;
                ccat.GHX_CONT_UOM3 = myObj.recordsets[0].GHX_CONT_UOM3;
                ccat.GHX_CONT_QOE3 = myObj.recordsets[0].GHX_CONT_QOE3;
                ccat.GHX_CONT_PRICE3 = myObj.recordsets[0].GHX_CONT_PRICE3;
                ccat.GHX_CONT_TYPE3 = myObj.recordsets[0].GHX_CONT_TYPE3;
                ccat.GHX_CONT_ID4 = myObj.recordsets[0].GHX_CONT_ID4;
                ccat.GHX_CONT_DESC4 = myObj.recordsets[0].GHX_CONT_DESC4;
                ccat.GHX_CONT_EFF_DATE4 = myObj.recordsets[0].GHX_CONT_EFF_DATE4;
                ccat.GHX_CONT_EXP_DATE4 = myObj.recordsets[0].GHX_CONT_EXP_DATE4;
                ccat.GHX_CONT_TIER4 = myObj.recordsets[0].GHX_CONT_TIER4;
                ccat.GHX_CONT_UOM4 = myObj.recordsets[0].GHX_CONT_UOM4;
                ccat.GHX_CONT_QOE4 = myObj.recordsets[0].GHX_CONT_QOE4;
                ccat.GHX_CONT_PRICE4 = myObj.recordsets[0].GHX_CONT_PRICE4;
                ccat.GHX_CONT_TYPE4 = myObj.recordsets[0].GHX_CONT_TYPE4;
                ccat.GHX_CONT_ID5 = myObj.recordsets[0].GHX_CONT_ID5;
                ccat.GHX_CONT_DESC5 = myObj.recordsets[0].GHX_CONT_DESC5;
                ccat.GHX_CONT_EFF_DATE5 = myObj.recordsets[0].GHX_CONT_EFF_DATE5;
                ccat.GHX_CONT_EXP_DATE5 = myObj.recordsets[0].GHX_CONT_EXP_DATE5;
                ccat.GHX_CONT_TIER5 = myObj.recordsets[0].GHX_CONT_TIER5;
                ccat.GHX_CONT_UOM5 = myObj.recordsets[0].GHX_CONT_UOM5;
                ccat.GHX_CONT_QOE5 = myObj.recordsets[0].GHX_CONT_QOE5;
                ccat.GHX_CONT_PRICE5 = myObj.recordsets[0].GHX_CONT_PRICE5;
                ccat.GHX_CONT_TYPE5 = myObj.recordsets[0].GHX_CONT_TYPE5;
                ccat.NUV_APRV_CONT_PRICE = myObj.recordsets[0].NUV_APRV_CONT_PRICE;
                ccat.GUDID_PRIMARY_DI = myObj.recordsets[0].GUDID_PRIMARY_DI;

                //}
                //reader.Close();
            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to retrieve CCAT item record: " + ex.Message);
            }

            return;
        }

        public static void PreprocessRetryItems()
        {
            try
            {
                //add Body parameter to REST request
                var paramBody = "{\"name\": \"Ccat_PreprocessRetryItems\"," +
                    "\"params\": []," +                 
                    "\"values\": []," +
                    "\"dataTypes\": []," +
                    "\"dbName\": \"\"," +
                    "\"schemaName\": \"Nuvia\"}";

                ClientConnectPost(paramBody);

            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Failed to execute Ccat_PreprocessRetryItems: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks to see if there are any records that need processing.
        /// </summary>
        /// <returns>True if records need processing, false if not.</returns>
        public bool AnyRecordsToProcess()
        {
            bool result = false;

            try
            {
                result = (SelectNextRecord() != null);
            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteError("Unable to check for records to process: " + ex.Message);
            }

            return result;
        }


    }
}
