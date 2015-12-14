﻿namespace ValidicCSharpApp.Models
{
    using System.Collections.Generic;

    using ValidicCSharp.Model;

    public class MainModel
    {
        public List<OrganizationAuthenticationCredentials> OrganizationAuthenticationCredentials { get; set; }

        public void Populate()
        {
            this.OrganizationAuthenticationCredentials = new List<OrganizationAuthenticationCredentials>
                                                             {
                                                                 new OrganizationAuthenticationCredentials
                                                                     {
                                                                         OrganizationId
                                                                             =
                                                                             "51aca5a06dedda916400002b",
                                                                         AccessToken
                                                                             =
                                                                             "ENTERPRISE_KEY"
                                                                     },
                                                                 new OrganizationAuthenticationCredentials
                                                                     {
                                                                         OrganizationId
                                                                             =
                                                                             "52e175c5e5af473f13000034",
                                                                         AccessToken
                                                                             =
                                                                             "8a54ead80e25826eac4c281d7f50e71a7a5778d4e776b0fc8f972c7ace908ad6"
                                                                     }
                                                             };
        }
    }
}