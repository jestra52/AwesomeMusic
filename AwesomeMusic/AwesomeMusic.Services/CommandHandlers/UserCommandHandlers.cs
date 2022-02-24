namespace AwesomeMusic.Services.CommandHandlers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AwesomeMusic.Data.Commands.UserCommands;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Data.Model.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class UserCommandHandlers
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IAwesomeMusicContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new Response<UserDto>();
                var user = _mapper.Map<User>(request);

                var isExistingUser = await _context.Users.AnyAsync(u => u.Email == request.Email.Trim(), cancellationToken);

                if (isExistingUser)
                {
                    response.Error = true;
                    response.Message = "Email is already registered";
                    return response;
                }

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveAsync(cancellationToken);
                response.Result = _mapper.Map<UserDto>(user);
                response.Message = "User created successfully";

                return response;
            }
        }

        public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthResponseDto>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly string _secretKey;

            public AuthenticateCommandHandler(IAwesomeMusicContext context, IConfiguration configuration)
            {
                _context = context;
                _secretKey = configuration["SecretKey"];
            }

            public async Task<Response<AuthResponseDto>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
            {
                var response = new Response<AuthResponseDto>();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email.Trim() && u.Password == request.Password, cancellationToken);

                if (user == null)
                {
                    response.Error = true;
                    response.Message = "Wrong email or password";
                    return response;
                }

                var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                });
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                response.Result = new AuthResponseDto { Token = tokenHandler.WriteToken(createdToken) };
                response.Message = "User authenticated successfully";

                return response;
            }
        }
    }
}
