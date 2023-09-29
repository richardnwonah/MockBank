using System;
using System.Threading;
using System.Threading.Tasks;
using CAVBackEndUpdate.Reopsitory;
using CAVBackEndUpdate.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CAVBackEndUpdate.Utils
{
    public class BackGroundTask : BackgroundService
    {
        private readonly ILogger<BackGroundTask> _logger;
        public IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        public BackGroundTask(IServiceProvider serviceProvider, ILogger<BackGroundTask> logger) //IUserService userService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
          //  _userService = userService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                DoWorkAsync();
                //_userService.GenerateUsers();
                await Task.Delay(TimeSpan.FromHours(10), stoppingToken);
            }
        }
            private async Task DoWorkAsync()
            {
                _logger.LogInformation(" working.");

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IUserService user = scope.ServiceProvider.GetRequiredService<IUserService>();

                 user.GenerateUsers();


            }

        }
       
    }
}
