using AES_Crypto_Example.Models.User;
using AES_Crypto_Example.ResponseModels;

namespace AES_Crypto_Example.Abstract
{
    public interface IUserService
    {
        //Girilen Kullanıcıyı Sisteme Kaydeder
        ResponseModel Register(UserModel model);

        //Parametrede verilen kullanıcının sisteme kayıtlı olup olmadığını kontrol eder.
        bool IsUserExist(string userName, string password, string serverName);

        //Sisteme Kayıtlı bütün kullanıcılarının listesini verir
        ResponseModelWithData<UserModel[]> GetUsers();

        //Parametrede id'si verilen kullanıcı sistemde kayıtlıysa bulur ve bilgilerini getirir.
        ResponseModelWithData<UserModel> FindUserWithId(int index);     

        //Parametrede yeni değerleri gelen kullanıcının id'si hariç değerleri güncellenir ve tekrar sisteme kaydedilir.
        ResponseModel UpdateUser(UserModel model);

        //Id'si verilen kullanıcıyı sistemden siler.
        ResponseModel DeleteUser(int id);

        //Sistemde an itibariyle kaç kullanıcı olduğu bilgisini dönderir.
        int FindNumberOfUsers();
    }
}
