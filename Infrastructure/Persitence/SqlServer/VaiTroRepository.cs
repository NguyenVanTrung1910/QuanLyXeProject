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
    public partial class VaiTroRepository : IVaiTroRepository
    {
        public List<VaiTro_Entity> GetPaged(VaiTroQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.VaiTros
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new VaiTro_Entity
                        {
                            Id = obj.Id,
                            TenVaiTro = obj.TenVaiTro,
                            MoTa = obj.MoTa,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                        };
            return  query.GetByGridRequest(SearchOption.oGridRequest, ref Total).ToList();

        }
        public List<VaiTroMenu> GetVaiTroMenuByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroMenus.Where(dk => dk.VaiTroId == VaiTroId).ToList();
            }
            catch { return null; }
        }
        public List<VaiTroQuyen> GetVaiTroQuyenByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroQuyens.Where(dk => dk.VaiTroId == VaiTroId).ToList();
            }
            catch { return null; }
        }
        public int DeleteQuyenByVaiTroId(int VaiTroId)
        {
            try
            {
                var context = (DBContext)UnitOfWork.Context;
                List<VaiTroQuyen> listEntity = this.GetQuyenByVaiTroId(VaiTroId);
                if (listEntity != null)
                    for (int i = 0; i < listEntity.Count; i++)
                    {
                        context.VaiTroQuyens.Remove(listEntity[i]);
                    }
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        public List<VaiTroQuyen> GetQuyenByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroQuyens.Where(dk => dk.VaiTroId == VaiTroId).ToList();
            }
            catch { return null; }
        }
        public List<VaiTroMenu> GeMenutByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroMenus.Where(dk => dk.VaiTroId == VaiTroId).ToList();
            }
            catch { return null; }
        }

        public int DeleteMenuByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                List<VaiTroMenu> listEntity = this.GeMenutByVaiTroId(VaiTroId);
                if (listEntity != null)
                    for (int i = 0; i < listEntity.Count; i++)
                    {
                        context.VaiTroMenus.Remove(listEntity[i]);
                    }
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        public void AddQuyenForVaiTro(VaiTroQuyen vaiTroQuyen)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.VaiTroQuyens.Add(vaiTroQuyen);
            context.SaveChanges();
        }
        public void AddMenuForVaiTro(VaiTroMenu vaiTroMenu)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.VaiTroMenus.Add(vaiTroMenu);
            context.SaveChanges();
        }
        public List<VaiTroMenu> GetMenuByVaiTroId(int VaiTroId)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTroMenus.Where(dk => dk.VaiTroId == VaiTroId).ToList();
            }
            catch { return null; }
        }

        public List<ItemTreeView> GetTreeVaiTroTreeView()
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.VaiTros
                        select new VaiTro_Entity
                        {
                            Id = obj.Id,
                            TenVaiTro = obj.TenVaiTro,
                            MoTa = obj.MoTa,
                            LastModifiedByText = obj.LastModifiedByText,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,


                        };
            List<VaiTro_Entity> itemsFromDatabase = query.ToList();

            return itemsFromDatabase
                   .Select(x => new ItemTreeView
                   {
                       id = x.Id.ToString(),
                       text = x.TenVaiTro,
                       ModerationStatus = (Int32)x.ModerationStatus,
                   })
                   .ToList();
        }

        public void DeleteByUserId(int userId)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            var item = context.VaiTroNguoiDungs.FirstOrDefault(a => a.UserId == userId);
            if (item != null)
            {
                context.VaiTroNguoiDungs.Remove(item);
                context.SaveChanges();
            }
        }

        public void AddVaiTroNguoiDung(VaiTroNguoiDung vaiTroNguoiDung)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.VaiTroNguoiDungs.Add(vaiTroNguoiDung);
            context.SaveChanges();
        }
    }
}
