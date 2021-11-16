using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rsk.TokenExchange;
using Rsk.TokenExchange.Validators.Adaptors;

namespace STS.Services
{
    public class ActorSupportingTokenExchangeClaimsParser : TokenExchangeClaimsParser
    {
        private readonly ITokenValidatorAdaptor _tokenValidator;
        public ActorSupportingTokenExchangeClaimsParser(ITokenValidatorAdaptor tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public override async Task<IEnumerable<Claim>> ParseClaims(IEnumerable<Claim> subjectClaims, ITokenExchangeRequest request)
        {
            if (subjectClaims == null) throw new ArgumentNullException(nameof(subjectClaims));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var claims = subjectClaims.ToList();

            var actor = new SubjectiveActor();

            if (!string.IsNullOrEmpty(request.ActorToken))
            {
                var actorToken = await _tokenValidator.ValidateAccessToken(request.ActorToken);
                if (!actorToken.IsError)
                {
                    actor.Subject = actorToken.Claims.First(a => a.Type == "sub").Value;
                }
            }

            if (request.ClientId != claims.FirstOrDefault(x => x.Type == "client_id")?.Value)
            {
                var previousActor = claims.FirstOrDefault(x => x.Type == "act");
                if (previousActor != null) claims.Remove(previousActor);

                actor.ClientId = request.ClientId;
                actor.InnerActor = previousActor?.Value;
                claims.Add(new Claim(
                    "act",
                    JsonConvert.SerializeObject(actor, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    JsonClaimValueTypes.Json));
            }

            return claims;
        }
    }
}