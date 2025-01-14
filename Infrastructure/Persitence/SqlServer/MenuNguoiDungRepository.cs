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
    public partial class MenuNguoiDungRepository : IMenuNguoiDungRepository
    {
        public List<MenuNguoiDung_Entity> GetPaged(MenuNguoiDungQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuNguoiDungs
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new MenuNguoiDung_Entity
                        {
                            Id = obj.Id,
                            ModerationStatus = obj.ModerationStatus,
                        };
            return  query.GetByGridRequest(SearchOption.oGridRequest, ref Total).ToList();

        }
        public List<MenuNguoiDung> GetListByUserId(int userId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.MenuNguoiDungs.Where(dk => dk.UserId == userId).ToList();
            }
            catch { return null; }
        }

        public void DeleteByUserId(int userId)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            var item = context.MenuNguoiDungs.FirstOrDefault(a => a.UserId == userId);
            if (item != null)
            {
                context.MenuNguoiDungs.Remove(item);
                context.SaveChanges();
            }
        }

        public void AddMenuNguoiDung(MenuNguoiDung menuNguoiDung)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.MenuNguoiDungs.Add(menuNguoiDung);
            context.SaveChanges();
        }
    }
}
