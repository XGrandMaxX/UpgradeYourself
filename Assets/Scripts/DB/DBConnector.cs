using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using UnityEngine.Networking;

namespace DB
{
    public sealed class DBConnector
    {
        private const string DB_USERS_URL = "https://learndb-7657c-default-rtdb.europe-west1.firebasedatabase.app/Users/";
        
        private readonly CancellationTokenSource _source = new();
        private readonly InputUIMessages _inputUIMessages;
        
        private UserData _existUser;
        private Dictionary<string, UserData> _allUsers;


        public DBConnector(InputUIMessages inputUIMessages) => _inputUIMessages = inputUIMessages;
        
        public async Task<UserData> LoginUserAsync(string login, string password)
        {
            bool userExists = await UserExistAsync(login);

            if (!userExists)
                _inputUIMessages.ThrowLoginErrorException("User not found, please sign up!");
            
            if (string.Equals(
                    _existUser.Login, login, StringComparison.CurrentCultureIgnoreCase) 
                 && _existUser.Password == HashPassword(password))
                return _existUser;
            
            _inputUIMessages.ThrowLoginErrorException("Invalid login or password");

            throw new InvalidOperationException();
        }
                
        public async Task CreateNewUserAsync(string login, string password)
        { 
            bool userExists = await UserExistAsync(login);
            
            if (userExists || string.Equals(_existUser.Login, login, StringComparison.CurrentCultureIgnoreCase))
                _inputUIMessages.ThrowRegisterErrorException("User with this login already exists");
            if(login.Length < 3)
                _inputUIMessages.ThrowRegisterErrorException("Minimum login length is 3 characters");
            if(password.Length < 4)
                _inputUIMessages.ThrowRegisterErrorException("Minimum password length is 4 characters");

            string hashedPassword = HashPassword(password);
            
            
            UserData newUser = new()
            {
                Exp = 0,
                Level = 0,
                Login = login,
                Password = hashedPassword,
                UpCoins = 0
            };
            
            string json = JsonConvert.SerializeObject(newUser);
            
            using UnityWebRequest putRequest = UnityWebRequest.Put(DB_USERS_URL + login + ".json", json);

            UnityWebRequestAsyncOperation operation = putRequest.SendWebRequest();

            while (!operation.isDone)
            {
                if(_source.Token.IsCancellationRequested)
                    return;
                
                await Task.Yield();
            }

            if (putRequest.result != UnityWebRequest.Result.Success)
                Debug.Log($"putRequest error: {putRequest.error}");
            else
            {
                _inputUIMessages.SetRegisterErrorMessage($"<color=green>User {newUser.Login} successfully registered.</color>");
                await Task.Delay(1000, _source.Token);
            }
        }

        //ADMIN FUNCTION
        public async Task SetNewPasswordAsync(string login, string newPassword)
        {
            bool userExists = await UserExistAsync(login);
            
            if(!userExists)
                throw new Exception($"user {login} has not found!");
            
            
            UserData existedUser = new()
            {
                Exp = _existUser.Exp,
                Level = _existUser.Level,
                Login = _existUser.Login,
                Password = HashPassword(newPassword),
                UpCoins = _existUser.UpCoins
            };
            
            string json = JsonConvert.SerializeObject(existedUser);

            using UnityWebRequest putRequest = UnityWebRequest.Put(DB_USERS_URL + login + ".json", json);
            
            UnityWebRequestAsyncOperation operation = putRequest.SendWebRequest();
                
            while (!operation.isDone)
            {
                if(_source.Token.IsCancellationRequested)
                    return;
                
                await Task.Yield();
            }
        }
        
        private string HashPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            foreach (var t in bytes)
                builder.Append(t.ToString("x2"));

            return builder.ToString();
        }
        
        private async Task<Dictionary<string, UserData>> GetAllUsersAsync()
        {
            using UnityWebRequest getRequest = UnityWebRequest.Get(DB_USERS_URL + ".json");
            
            UnityWebRequestAsyncOperation operation = getRequest.SendWebRequest();

            while (!operation.isDone)
            {
                if(_source.Token.IsCancellationRequested)
                    return null;
                
                await Task.Yield();
            }

            if (getRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"getRequest error: {getRequest.error}, return null UserData array");
                return null;
            }

            string json = getRequest.downloadHandler.text;
            
            if (json == "null")
                return null;
                    
            Dictionary<string, UserData> users = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);
            
            return users;
        }
        

        private async Task<bool> UserExistAsync(string login)
        {
            Dictionary<string, UserData> users = await GetAllUsersAsync();
            
            if (users == null || !users.TryGetValue(login, out UserData user)) 
                return false;
            
            _existUser = user;
            return true;
        }
    }

    [System.Serializable]
    public struct UserData
    {
        public string Login;
        [HideInInspector] public string Password;
        
        public int Exp;
        public int Level;
        
        public int UpCoins;

        public int PhysicalAbilities;
        public int IntellectualAbilities;
    }
}
