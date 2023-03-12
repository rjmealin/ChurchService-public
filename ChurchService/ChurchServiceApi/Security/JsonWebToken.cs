using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChurchServiceApi.Security
{
    public class JsonWebToken
    {
        public enum BadTokenTypes
        {
            None,
            Missing,
            Invalid,
            Expired,
        }

        private bool _hasTokenValue = false;
        public bool HasTokenValue { get { return _hasTokenValue; } }

        private bool _emailVerified = false;
        public bool EmailVerified { get { return _emailVerified; } }

        private bool _isAdmin = false;
        public bool IsAdmin { get { return _isAdmin; } }



        private string _fullName = "";
        public string FullName { get { return _fullName; } }
        public Guid UserId { get; }
        public Guid ChurchId { get; }

        private string errorMessage = "The access token is missing or invalid.";

        private JwtSecurityToken token;










        /// <summary>
        /// Returns a WWW Authenticate header for the given token error.
        /// </summary>
        /// <param name="badTokenType"></param>
        /// <param name="errorDescription"></param>
        /// <returns></returns>
        public string GetWWWAuthenticateHeaderValue(BadTokenTypes badTokenType, string errorDescription = null)
        {
            var error = "invalid_token";

            if (badTokenType == BadTokenTypes.Missing)
                error = "token";
            else if (badTokenType == BadTokenTypes.Invalid)
                error = "invalid_token";
            else if (badTokenType == BadTokenTypes.Expired)
                error = "Expired_Token";
            errorDescription = errorDescription ?? errorMessage;

            return $"Bearer realm=\"{Resources.AppSettings.JwtIssuer}\", error=\"{error}\", error_description=\"{errorDescription}\"";
        }










        /// <summary>
        /// Instantiates a new json web token with the given encrypted header value.
        /// </summary>
        /// <param name="authorizationHeader"></param>
        public JsonWebToken(System.Net.Http.Headers.AuthenticationHeaderValue authorizationHeader)
        {
            if (authorizationHeader != null)
            {
                _hasTokenValue = true;

                try
                {
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Resources.AppSettings.JwtBase64Secret));
                    var tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudiences = new string[] { Resources.AppSettings.JwtAudience },
                        ValidIssuers = new string[] { Resources.AppSettings.JwtIssuer },
                        IssuerSigningKey = signingKey
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    tokenHandler.ValidateToken(authorizationHeader.ToString(), tokenValidationParameters, out SecurityToken validatedToken);

                    if (validatedToken is JwtSecurityToken)
                    {
                        token = validatedToken as JwtSecurityToken;

                        token.Payload.TryGetValue("emailVerified", out object emailVerified);

                        if (emailVerified != null)
                            bool.TryParse(emailVerified.ToString(), out _emailVerified);

                        token.Payload.TryGetValue("isAdmin", out object isAdmin);

                        if (isAdmin != null)
                            bool.TryParse(isAdmin.ToString(), out _isAdmin);


                        // FullName
                        token.Payload.TryGetValue("fullName", out object fullName);

                        if (fullName != null)
                            _fullName = fullName.ToString();

                        // UserId
                        if (token.Payload.TryGetValue("userId", out object userId))
                            UserId = new Guid((string)userId);

                        // churchid
                        if (token.Payload.TryGetValue("churchId", out object churchId))
                            ChurchId = new Guid((string)churchId);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }

            }

        }










        /// <summary>
        ///     Creates and returns a new json web token.
        /// </summary>
        /// 
        /// <param name="userId">The database id of the user.</param>
        /// <param name="role">The user's role.</param>
        /// <param name="fullName">The full name of the user.</param>
        /// <param name="authed">True if the name/password combo are valid</param>
        /// 
        /// <returns>
        ///     Returns the signed json web token.
        /// </returns>
        /// 
        public static string GetUserToken(Guid userId, string fullName, bool emailVerified, bool admin, Guid churchId)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Resources.AppSettings.JwtBase64Secret));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("userId", userId.ToString()),
                new Claim("emailVerified", emailVerified ? "true" : "false"),
                new Claim("isAdmin", admin ? "true" : "false"),
                new Claim("fullName", fullName),
                new Claim("churchId", churchId.ToString())
            }, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = Resources.AppSettings.JwtAudience,
                Issuer = Resources.AppSettings.JwtIssuer,
                Subject = claimsIdentity,
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(7)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return signedAndEncodedToken;
        }

    }
}
