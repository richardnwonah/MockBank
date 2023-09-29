using CAVBackEndUpdate.Models;
using System;
using RandomNameGeneratorLibrary;
using Microsoft.Extensions.Logging;
using CAVBackEndUpdate.Models.Services;
using CAVBackEndUpdate.Reopsitory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CAVBackEndUpdate.Services
{
    public partial class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private  IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository) { 
      
           _logger = logger;
            _userRepository = userRepository;
        }


        string currentState;
        string currentCity;
        public User Createuser()
        {
         
           IPersonNameGenerator personNameGenerator = new PersonNameGenerator();
            
           IPlaceNameGenerator place = new PlaceNameGenerator();

            var user = new User();
           
            user.FirstName = personNameGenerator.GenerateRandomFirstName();
            user.LastName = personNameGenerator.GenerateRandomLastName();  
            user.AccountNumber = GenerateAccountNumber();
            user.Address = RandomAddressGenerator();
            user.State = GetStates();
            user.City = GetCiteByState(user.State);  
            user.Currency = "NGN";
            user.CumulativeInflow = GenerateAccountBalance();
            user.CreatedAt = DateTime.Now;
            return user;
            
           
        }

        public int GenerateAccountBalance()
        {
            var balance = 0;
            Random random = new Random();
            balance = random.Next(500, 3000);
            return balance;
        }
        public string RandomAddressGenerator()
        {  
            string[] addressList = new string[31] {Address.A1,
            Address.A2, Address.A3, Address.A4, Address.A5, Address.A6, Address.A7,
            Address.A8,  Address.A9,   Address.A10, Address.A11, Address.A12,Address.A13,
            Address.A14, Address.A15, Address.A16, Address.A17,Address.A18, Address.A19,
            Address.A20,  Address.A21, Address.A22, Address.A23,Address.A24,  Address.A25,
            Address.A26,  Address.A27, Address.A28, Address.A29,  Address.A30,  Address.A31
            };

            Random rand = new Random();

            int index = rand.Next(addressList.Length);
            int houseNum = rand.Next(0, 100);

            var address =  addressList[index];

            var finalAddress = houseNum + "," + address;

            return finalAddress;
        }

        private string GenerateAccountNumber ()
        {
           string AccountNumberGenerated;
            var rand = new Random();
            AccountNumberGenerated = Convert.ToString((long)Math.Floor(rand.NextDouble() * 9_000_000_000L + 1_000_000_000L));
            return AccountNumberGenerated;

        }
        public string GetStates()
        {
            var rand = new Random();
            Type type = typeof(States);
            Array state = type.GetEnumValues();

            for (int i = 0; i < 1; ++i)
            {
                int index = rand.Next(state.Length);
                States value = (States)state.GetValue(index);
                var State = value.ToString();
                currentState = State;
            }
            return currentState;
        }

        public string GetCiteByState(string currentState)
        {
           
          switch (currentState)
            {
                case "Lagos":
                    CitySort<CitiesInLagos>();
                    break;

                case "Abuja":
                    CitySort<CitiesInAbuja>();
                    break;

                case "Rivers":
                    CitySort<CitiesInRivers>();
                    break;

                default: throw new ArgumentException();

            }
            return currentCity;
            
        }

        public string CitySort<T>() 
        {
            var rand = new Random();
            Type type = typeof(T);
            Array city = type.GetEnumValues();

            for (int i = 0; i < 1; ++i)
            {
                int index = rand.Next(city.Length);
                T value = (T)city.GetValue(index);
                var City = value.ToString();
                currentCity = City;
            }
            return currentCity;
        }

        public void GenerateUsers()
        { 
        for (int i = 0; i < 300; i++)
        {
               
                var user = Createuser();
              _userRepository.AddUsersToDb(user);
            }
       
        }
        
        public List<User> GetUsersFromDb(AccountDateParameter daterange)
        {
            var UserList = _userRepository.GetAllUsersByDate(daterange);
                return UserList;
        }
        public List<User> GetUsersDataWithOutDate()
        {
            var UserListWithOutDate = _userRepository.GetAllUsers();
            return UserListWithOutDate;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetById(userId);
        }

    }
}
