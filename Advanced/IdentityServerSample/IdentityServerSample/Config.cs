using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerSample
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new[]
            {
                new ApiResource("api", "My Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //ClientCredential模式
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret = false,//如果信得过的第三方可以不用secret，防止客户端被抓包
                    AllowedScopes = {"api"}
                },
                //用户密码模式
                new Client
                {
                    ClientId = "pwdClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api"}
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "free",
                    Password = "123456"
                }
            };
        }
    }
}