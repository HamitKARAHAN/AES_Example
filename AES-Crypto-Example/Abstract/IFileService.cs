using AES_Crypto_Example.ResponseModels;

namespace AES_Crypto_Example.Abstract
{
    public interface IFileService
    {
        ResponseModelWithData<string[]> ReadFile(string path, string fileName);

        ResponseModel WriteToFile(string path, string fileName, string content);

        ResponseModel WriteKeyToFile(string path, string UserKeyName);

        ResponseModelWithData<string> ReadKeyFromFile(string path, string fileName, int index);

        ResponseModel UpdateUser(string path, string fileName, string[] users, string content, int id);

        ResponseModel DeleteUser(string path, string fileName, int id);

        int FindNumberOfUsers(string absolutePath);

        bool DeleteFile(string path, string usersFileName, string keysFileName);
    }
}
