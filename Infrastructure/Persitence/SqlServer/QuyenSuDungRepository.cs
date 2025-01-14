using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class QuyenSuDungRepository : IQuyenSuDungRepository
    {
        public List<QuyenSuDung_Entity> GetPaged(QuyenSuDungQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.QuyenSuDungs
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new QuyenSuDung_Entity
                        {
                            Id = obj.Id,
                            MaQuyen = obj.MaQuyen,
                            TenQuyen = obj.TenQuyen,
                            //IdNguoiDung = obj.IdNguoiDung,
                            MoTa = obj.MoTa,
                            //IdQuyenSuDung = obj.IdQuyenSuDung,
                            //TenNguoiDung  = obj.NguoiDung.TenNguoiDung,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                        };
            return  query.GetByGridRequest(SearchOption.oGridRequest, ref Total).ToList();

        }
        public List<ItemTreeView> GetTreeQuyenTreeView()
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.QuyenSuDungs
                        select new QuyenSuDung_Entity
                        {
                            Id = obj.Id,
                            MaQuyen = obj.MaQuyen,
                            TenQuyen = obj.TenQuyen,
                            MoTa = obj.MoTa,
                            //Created = obj.Created,
                            //CreatedBy = obj.CreatedBy,
                            //LastModified = obj.LastModified,
                            LastModifiedByText = obj.LastModifiedByText,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                            QuyenSuDungCha = obj.QuyenSuDungCha,


                        };
            List<QuyenSuDung_Entity> itemsFromDatabase = query.ToList();

            return itemsFromDatabase
                   .Where(x => x.QuyenSuDungCha == null) // Lọc ra các menu cha (MenuChaId == null)
                   .Select(x => new ItemTreeView
                   {
                       id = x.Id.ToString(),
                       text = x.TenQuyen + "(" + x.MaQuyen + ")",
                       ModerationStatus = (Int32)x.ModerationStatus,
                       children = GetChildren(itemsFromDatabase, x.Id) // Lấy danh sách con của menu cha
                   }).OrderBy(x => x.text)
                   .ToList();
        }
        private List<ItemTreeView> GetChildren(List<QuyenSuDung_Entity> items, int quyenChaId)
        {
            return items
                .Where(x => x.QuyenSuDungCha == quyenChaId)
                .Select(x => new ItemTreeView
                {
                    id = x.Id.ToString(),
                    text = x.TenQuyen + "(" + x.MaQuyen + ")",
                    ModerationStatus = (Int32)x.ModerationStatus,
                    children = GetChildren(items, x.Id) // Lấy danh sách con của menu hiện tại
                })
                .ToList();
        }
        public List<int> GetAllQuyenCha()
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = context.QuyenSuDungs
                .Where(m => m.QuyenSuDungCha == null)
                .Select(m => m.Id)
                .ToList();
            return query;
        }
        public bool CheckHaveSubItem(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.QuyenSuDungs
                        where obj.QuyenSuDungCha == Id
                        select obj;
            return query.Any();
        }
        public List<PhanQuyenNguoiDung> GetListQuyenByUserId(int userId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.PhanQuyenNguoiDungs.Where(dk => dk.UserId == userId).ToList();
            }
            catch { return new List<PhanQuyenNguoiDung>(); }
        }        
        public List<VaiTroQuyen> GetListQuyenByVaiTroId(int vaitroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroQuyens.Where(dk => dk.VaiTroId == vaitroId).ToList();
            }
            catch { return new List<VaiTroQuyen>(); }
        }
        public void DeleteByUserId(int userId)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            var item = context.PhanQuyenNguoiDungs.FirstOrDefault(a => a.UserId == userId);
            if (item != null)
            {
                context.PhanQuyenNguoiDungs.Remove(item);
                context.SaveChanges();
            }
        }

        public void AddPhanQuyenNguoiDung(PhanQuyenNguoiDung phanQuyenNguoiDung)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.PhanQuyenNguoiDungs.Add(phanQuyenNguoiDung);
            context.SaveChanges();
        }
    }
}
