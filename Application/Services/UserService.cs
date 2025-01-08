using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public UserService(ResponeActionResult responeActionResult,IUserRepository UserRepository) 
        {
            _UserRepository = UserRepository;
            _responeActionResult = responeActionResult;
        }

        public User GetById(int id)
        {
            return _UserRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _UserRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _UserRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(UserQuery searchQuery)
        {
           
            List<User_Entity> listTaiLieuThucTap = _UserRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _UserRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _UserRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(User item)
        {
            item.FillDataForInsert(1);
            _UserRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new User() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _UserRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(User item)
        {
            item.FillDataForUpdate(1);
            _UserRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new User() { Id = item.Id };
            return _responeActionResult;
        }
        public User_Entity GetEntity(int Id)
        {
            return _UserRepository.GetEntity(Id);
        }



    }
}
