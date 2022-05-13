using AES_Crypto_Example.Abstract;
using AES_Crypto_Example.GlobalSettings;
using AES_Crypto_Example.ResponseModels;
using System;
using System.IO;
using System.Linq;

namespace AES_Crypto_Example.Concrete
{
    public class FileService : IFileService
    {
        private readonly EncryptionService _encryptionService;
        
        public FileService(EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public  ResponseModel WriteToFile(string path, string fileName, string content)
        {
            string UserKeyName = EncryptionSettings.RandomString(8);

            WriteKeyToFile(path,UserKeyName);
            var absolutePath = path + fileName;
            try
            {               
                using (StreamWriter streamWriter= new StreamWriter(absolutePath, true))
                {
                    string str = _encryptionService.Encrypt(content, UserKeyName);
                    streamWriter.WriteLine(str);
                    return new ResponseModel("Writing successful!", true);
                }
            }
            catch (Exception e)
            {
                return new ResponseModel($"An error occured! : {e.Message}", false);
                throw;
            }
        }

        public ResponseModelWithData<string[]> ReadFile(string path, string fileName)
        {
            try
            {           
                var absolutePath = path + fileName;
                if (File.Exists(absolutePath))
                {
                    var lineCount= FindNumberOfUsers(absolutePath);
                    if(lineCount==0)
                    {
                        return new ResponseModelWithData<string[]>("There is no users in file!", false, null);
                    }
                    string[] line = new string[lineCount];
                    string[] data = new string[lineCount];
                    for (int i = 0; i < lineCount; i++)
                    {
                        var response = ReadKeyFromFile(path, GlobalFileNames.UserKeyFileName, i);
                        string x = response.Data;
                        line[i] = File.ReadLines(absolutePath).Skip(i).Take(1).First();
                        data[i] = _encryptionService.Decrypt(line[i], x);
                    }
                    return new ResponseModelWithData<string[]>("Reading is successful!", true, data);
                }
                return new ResponseModelWithData<string[]>("File couldn't found!", false, null);
            }
            catch (Exception e)
            {
                return new ResponseModelWithData<string[]>($"An error occured! :{e.Message}", false, null);
                throw;
            }
        }

        public ResponseModel WriteKeyToFile(string path, string UserKeyName)
        {
            var absolutePath = path + GlobalFileNames.UserKeyFileName;
            using (StreamWriter streamWriter = new StreamWriter(absolutePath, true))
            {
                streamWriter.WriteLine(UserKeyName);
                return new ResponseModel("Writing successful!", true);
            }
        }

        public ResponseModelWithData<string> ReadKeyFromFile(string path, string fileName, int index)
        {
            try
            {
                var absolutePath = path + GlobalFileNames.UserKeyFileName;
                if (File.Exists(absolutePath))
                {
                    string line = File.ReadLines(absolutePath).Skip(index).Take(1).First();
                    return new ResponseModelWithData<string>("Reading successful!", true, line);
                }
                return new ResponseModelWithData<string>("File couldn't found!", false, null);
            }
            catch (Exception e)
            {
                return new ResponseModelWithData<string>($"An error occured! :{e.Message}", false, null);
                throw;
            }
        }

        public ResponseModel UpdateUser(string path, string fileName, string[] users, string content, int id)
        {       
            var absolutePath = path + fileName;
            string[] UserKeyName = new string[users.Length];
            for (int i = 0; i < users.Length; i++)
            {
                UserKeyName[i] = EncryptionSettings.RandomString(8);
                users[i] = _encryptionService.Encrypt(users[i], UserKeyName[i]);
            }
            File.WriteAllLines(absolutePath, users);
            File.WriteAllLines(path + GlobalFileNames.UserKeyFileName, UserKeyName);
            
            return new ResponseModel("User updated successfully!", true);
        }
        
        public ResponseModel DeleteUser(string path, string fileName, int id)
        {
            try
            {
                var absolutePath = path + fileName;
                if (File.Exists(absolutePath))
                {
                    string[] userLines = File.ReadAllLines(absolutePath);
                    string[] keyLines = File.ReadAllLines(path + GlobalFileNames.UserKeyFileName);
                    DeleteFile(path, fileName, GlobalFileNames.UserKeyFileName);
                    using (StreamWriter sw = File.AppendText(absolutePath))
                    {
                        for (int i = 0; i < userLines.Length; i++)
                        {
                            if (i == id - 1)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(userLines[i]);
                            }
                        }

                    }
                    using (StreamWriter sw = File.AppendText(path + GlobalFileNames.UserKeyFileName))
                    {
                        for (int j = 0; j < keyLines.Length; j++)
                        {
                            if (j == id - 1)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(keyLines[j]);
                            }
                        }
                    }
                }
                else
                {
                    return new ResponseModel("File cannot be found", false);
                }
            }
            catch (Exception e)
            {
                return new ResponseModel($"An error occured:{e.Message}", false);
                throw;
            }
            return new ResponseModel("User deleted succesFully", true);
        }

        public int FindNumberOfUsers(string absolutePath)
        {
            int lineCount = 0;
            try
            {
                lineCount = File.ReadLines(absolutePath).Count();
            }
            catch (Exception)
            {
                return lineCount;
            }
            return lineCount;
        }

        public bool DeleteFile(string path, string usersFileName, string keysFileName)
        {
            var absoluteUsersPath = path + usersFileName;
            var absoluteKeysPath = path + keysFileName;
            try
            {
                if (File.Exists(absoluteUsersPath) && File.Exists(absoluteUsersPath))
                {
                    File.Delete(absoluteUsersPath);
                    File.Delete(absoluteKeysPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
