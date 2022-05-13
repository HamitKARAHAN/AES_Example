using System.IO;
using System.Text.Json;
using AES_Crypto_Example.Abstract;
using AES_Crypto_Example.GlobalSettings;
using AES_Crypto_Example.Models.User;
using AES_Crypto_Example.ResponseModels;

namespace AES_Crypto_Example.Concrete
{
    public class UserService : IUserService
    {
        private readonly FileService _fileService;
        public UserService(FileService fileService)
        {
            _fileService = fileService;
        }

        public ResponseModel Register(UserModel model)
        {
            if (!IsUserExist(model.Username,model.Password,model.ServerName))
            {
                var jsonData = JsonSerializer.Serialize(model);

                var response = _fileService.WriteToFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName, jsonData);
                if (response.Status)
                {
                    return new ResponseModel("User Registered!", true);
                }
                return response;
            }
            else
                return new ResponseModel("User is already registered!", false);
        }

        public bool IsUserExist(string userName, string password, string serverName)
        {
            if (!Directory.Exists(GlobalFileNames.FilePath))
            {
                Directory.CreateDirectory(GlobalFileNames.FilePath);
            }

            var fileData = _fileService.ReadFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName);
            if (fileData.Status)
            {
                for (int i = 0; i < fileData.Data.Length; i++)
                {
                    //Bulunan Kullanıcı Listesini Gez ve verilen kullanıcı var mı kontrol et
                    var userData = JsonSerializer.Deserialize<UserModel>(fileData.Data[i]);
                    if (userName.Equals(userData.Username) && password.Equals(userData.Password) && serverName.Equals(userData.ServerName))
                    {
                        return true;
                    }
                }
                return false;
            }
            return fileData.Status;
        }

        public ResponseModelWithData<UserModel> FindUserWithId(int index)
        {
            if (!Directory.Exists(GlobalFileNames.FilePath))
            {
                Directory.CreateDirectory(GlobalFileNames.FilePath);
            }

            var fileData = _fileService.ReadFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName);
            if (fileData.Status)
            {
                for (int i = 0; i < fileData.Data.Length; i++)
                {
                    var userData = JsonSerializer.Deserialize<UserModel>(fileData.Data[i]);
                    if (index.Equals(userData.ID))
                    {
                        return new ResponseModelWithData<UserModel>("User" + index + " is found!", true, userData);
                    }
                }
                return new ResponseModelWithData<UserModel>("User" + index + " coulnd't found!", false, null);
            }
            else
            {
                return new ResponseModelWithData<UserModel>("There is no user! Now you can try to add a new user!", false, null);
            }
        }

        public ResponseModelWithData<UserModel[]> GetUsers()
        {
            if (!Directory.Exists(GlobalFileNames.FilePath))
            {
                Directory.CreateDirectory(GlobalFileNames.FilePath);
            }

            var fileData = _fileService.ReadFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName);      
            if (fileData.Status)
            {
                UserModel[] userData = new UserModel[fileData.Data.Length];
                for (int i = 0; i < fileData.Data.Length; i++)
                {
                    userData[i] = JsonSerializer.Deserialize<UserModel>(fileData.Data[i]);
                }
                return new ResponseModelWithData<UserModel[]>("Users found!", true, userData);
            }
            else
            {
                return new ResponseModelWithData<UserModel[]>("Ther is no user!", false, null);
            }
        }

        public ResponseModel UpdateUser(UserModel model)
        {
            if (!Directory.Exists(GlobalFileNames.FilePath))
            {
                Directory.CreateDirectory(GlobalFileNames.FilePath);
            }
            var usersfileData = _fileService.ReadFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName);

            if (usersfileData.Status)
            {
                string jsonData = string.Empty;
                for (int i = 0; i < usersfileData.Data.Length; i++)
                {

                    var userData = JsonSerializer.Deserialize<UserModel>(usersfileData.Data[i]);

                    if(model.Username == userData.Username && model.Password == userData.Password && model.ServerName == userData.ServerName)
                    {
                        return new ResponseModel("This user is already registered before!", false);
                    }

                    if (model.ID.Equals(userData.ID))
                    {
                        jsonData = JsonSerializer.Serialize(model);
                        usersfileData.Data[i] = jsonData;
                    }
                }

                _fileService.UpdateUser(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName, usersfileData.Data, jsonData, model.ID);
            }
            else
            {
                return new ResponseModel("This block never works :) ", false);
            }
            return new ResponseModel("The user is updated successfully!", true);
        }

        public ResponseModel DeleteUser(int id)
        {        
            if (!Directory.Exists(GlobalFileNames.FilePath))
            {
                Directory.CreateDirectory(GlobalFileNames.FilePath);
            }

            var findUser = FindUserWithId(id);
            if(!findUser.Status)
            {
                return new ResponseModel("User " + id + " couldn't found!", false);
            }

            var fileData = _fileService.DeleteUser(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName, id);
            if(fileData.Status)
            {
                var userResponse = GetUsers();
                if (userResponse.Data==null)
                {
                    return new ResponseModel("There is no user for this operation!", true);
                }

                _fileService.DeleteFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName, GlobalFileNames.UserKeyFileName);
                var response= new ResponseModel("", true);
                string[] JsonData =new string[userResponse.Data.Length];
                for (int i = 0; i < userResponse.Data.Length; i++)
                {
                    if (userResponse.Data[i].ID > id)
                    {
                        userResponse.Data[i].ID -= 1;
                    }
                    JsonData[i] = JsonSerializer.Serialize(userResponse.Data[i]);
                    response = _fileService.WriteToFile(GlobalFileNames.FilePath, GlobalFileNames.UsersFileName, JsonData[i]);
                }               
                if (response.Status)
                {
                    return new ResponseModel("The user is deleted successfully!", true);
                }
                else
                {
                    return new ResponseModel("User couldn't delete!", false);
                }              
            }
            else
            {
                return new ResponseModel("An Error occured!", false);          
            }
        }

        public int FindNumberOfUsers()
        {
            int numberOfUsers = _fileService.FindNumberOfUsers(GlobalFileNames.FilePath + GlobalFileNames.UsersFileName);
            return numberOfUsers;
        }
    }
}
