//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyClaimsAuthorizationManager
//{
//    class ZipClaimsAuthorizationManager : ClaimsAuthorizationManager 
//    { 
//        private static Dictionary<string, int> m_policies = new Dictionary<string, int>(); 

//        public ZipClaimsAuthorizationManager(object config) 
//        { 
//            XmlNodeList nodes = config as XmlNodeList; 
//            foreach (XmlNode node in nodes) 
//            { 
//                { 
//                    //FIND ZIP CLAIM IN THE POLICY IN WEB.CONFIG AND GET ITS VALUE 
//                    //ADD THE VALUE TO MODULE SCOPE m_policies 
//                    XmlTextReader reader = new XmlTextReader(new StringReader(node.OuterXml)); 
//                    reader.MoveToContent(); 
//                    string resource = reader.GetAttribute("resource"); 
//                    reader.Read(); 
//                    string claimType = reader.GetAttribute("claimType"); 
//                    if (claimType.CompareTo(ClaimTypes.PostalCode) == 0) 
//                    { 
//                        throw new ArgumentNullException("Zip Authorization is not specified in policy in web.config"); 
//                    } 
//                    int zip = -1; 
//                    bool success = int.TryParse(reader.GetAttribute("Zip"),out zip); 
//                    if (!success) 
//                    { 
//                        throw new ArgumentException("Specified Zip code is invalid - check your web.config"); 
//                    } 
//                    m_policies[resource] = zip; 
//                } 
//            } 
//        } 
//        public override bool CheckAccess(AuthorizationContext context) 
//        { 
//            //GET THE IDENTITY 
//            //FIND THE POSTALCODE CLAIM'S VALUE IN IT 
//            //COMPARE WITH THE POLICY 
//            int allowedZip = -1; 
//            int requestedZip = -1; 
//            Uri webPage = new Uri(context.Resource.First().Value); 
//            IClaimsPrincipal principal = (IClaimsPrincipal)HttpContext.Current.User; 
//            if (principal == null) 
//            { 
//                throw new InvalidOperationException("Principal is not populate in the context - check configuration"); 
//            } 
//            IClaimsIdentity identity = (IClaimsIdentity)principal.Identity; 
//            if (m_policies.ContainsKey(webPage.PathAndQuery)) 
//            { 
//                allowedZip = m_policies[webPage.PathAndQuery]; 
//                requestedZip = -1; 
//                int.TryParse((from c in identity.Claims 
//                                        where c.ClaimType == ClaimTypes.PostalCode 
//                                        select c.Value).FirstOrDefault(), out requestedZip); 
//            } 
//            if (requestedZip!=allowedZip) 
//            { 
//                return false; 
//            } 
//            return true; 
//        } 
//    } 
//}
