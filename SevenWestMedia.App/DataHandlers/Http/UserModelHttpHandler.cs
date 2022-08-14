using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SevenWestMedia.App.DataHandlers.Interfaces.Http;
using SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers;
using SevenWestMedia.App.Validation;
using SevenWestMedia.Common.DTOs;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.DataHandlers.Http
{
    public class UserModelHttpHandler : IModelHandler<User>
    {
        private readonly IHttpHandler<UserDTO> _httpHandler;
        private readonly IValidationService<User> _userValidationService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserModelHttpHandler(IHttpHandler<UserDTO> httpHandler, IValidationService<User> userValidationService, IConfiguration configuration, IMapper mapper, ILogger<UserModelHttpHandler> logger)
        {
            _httpHandler = httpHandler;
            _userValidationService = userValidationService;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IList<User>> GetDataAsync()
        {
            IList<User> users = new List<User>();
            string url = _configuration["UserData:apiUrl"];
            IList<User> allUsers = _mapper.Map<IEnumerable<User>>(await _httpHandler.GetAsync(url)).ToList();
            foreach (User user in allUsers)
            {
                if (_userValidationService.IsModelValid(user, out IList<ValidationResult> validationResults))
                {
                    users.Add(user);
                }
                else
                {
                    _logger.LogWarning($"User cannot be saved. User => {user} is not valid. " +
                        $"Errors: {string.Join("-", validationResults.Select(x => x.ErrorMessage))}");   
                }
            }
            return users;
        }
    }
}