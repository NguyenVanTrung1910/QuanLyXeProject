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
    public partial class MenuQuanTriRepository : IMenuQuanTriRepository
    {
        public List<MenuQuanTri_Entity> GetPaged(MenuQuanTriQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuQuanTris
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new MenuQuanTri_Entity
                        {
                            Id = obj.Id,
                            TenMenu = obj.TenMenu,
                            ModerationStatus = obj.ModerationStatus,
                            Category = obj.Category,
                            MenuChaId = obj.MenuChaId,
                            ViTri = obj.ViTri,
                            Icon = obj.Icon,
                            LienKet = obj.LienKet,
                        };
            return  query.GetByGridRequest(SearchOption.oGridRequest, ref Total).ToList();

        }
        public List<TreeMenuQuanTri> GetTreeMenu()
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuQuanTris
                        where obj.ModerationStatus == ModerationStatus.Approved
                        orderby obj.ViTri//__H__
                        select new MenuQuanTri_Entity
                        {
                            Id = obj.Id,
                            TenMenu = obj.TenMenu,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                            Category = obj.Category,
                            MenuChaId = obj.MenuChaId,
                            ViTri = obj.ViTri,
                            Icon = obj.Icon,
                            LienKet = obj.LienKet,
                        };

            List<MenuQuanTri_Entity> itemsFromDatabase = query.ToList();



            return itemsFromDatabase
                   .Where(x => x.MenuChaId == null)
                    .OrderBy(x => x.ViTri) // Sắp xếp các menu cha theo ViTri
                   .Select(obj => new TreeMenuQuanTri
                   {
                       Id = obj.Id,
                       TenMenu = obj.TenMenu,
                       ModerationStatus = obj.ModerationStatus,
                       ModerationStatusTxt = obj.ModerationStatusTxt,
                       Category = obj.Category,
                       MenuChaId = obj.MenuChaId,
                       ViTri = obj.ViTri,
                       Icon = obj.Icon,
                       LienKet = obj.LienKet,
                       children = GetChildrenForTreeMenu(itemsFromDatabase, obj.Id)
                   })
                   .ToList();
        }

        private List<TreeMenuQuanTri> GetChildrenForTreeMenu(List<MenuQuanTri_Entity> items, int menuChaId)
        {
            return items
                .Where(x => x.MenuChaId == menuChaId)
                .OrderBy(x => x.ViTri) // Sắp xếp các menu con theo ViTri
                .Select(obj => new TreeMenuQuanTri
                {
                    Id = obj.Id,
                    TenMenu = obj.TenMenu,
                    ModerationStatus = obj.ModerationStatus,
                    ModerationStatusTxt = obj.ModerationStatusTxt,
                    Category = obj.Category,
                    MenuChaId = obj.MenuChaId,
                    ViTri = obj.ViTri,
                    Icon = obj.Icon,
                    LienKet = obj.LienKet,
                    children = GetChildrenForTreeMenu(items, obj.Id) // Lấy danh sách con của menu hiện tại
                })
                .ToList();
        }

        public List<int> GetMenuChaByMenuConId(List<int> listId)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < listId.Count; i++)
            {
                int menuChaId = (int)this.GetById(listId[i]).MenuChaId;
                result.Add(menuChaId);
            }
            return result.Distinct().ToList();
        }

        /// <summary>
        /// lấy danh sách phục vụ dữ liệu cho treeview
        /// </summary>
        /// <returns></returns>
        public List<ItemTreeView> GetTreeMenuTreeView()
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuQuanTris
                        orderby obj.ViTri//__H__
                        select new MenuQuanTri_Entity
                        {
                            Id = obj.Id,
                            TenMenu = obj.TenMenu,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                            Category = obj.Category,
                            MenuChaId = obj.MenuChaId,
                            ViTri = obj.ViTri,
                            Icon = obj.Icon,
                            LienKet = obj.LienKet,


                        };
            List<MenuQuanTri_Entity> itemsFromDatabase = query.ToList();

            return itemsFromDatabase
                   .Where(x => x.MenuChaId == null) // Lọc ra các menu cha (MenuChaId == null)
                    .OrderBy(x => x.ViTri) //__H__
                   .Select(x => new ItemTreeView
                   {
                       id = x.Id.ToString(),
                       text = x.TenMenu,
                       ModerationStatus = (Int32)x.ModerationStatus,
                       children = GetChildren(itemsFromDatabase, x.Id) // Lấy danh sách con của menu cha
                   })
                   .ToList();
        }

        private List<ItemTreeView> GetChildren(List<MenuQuanTri_Entity> items, int menuChaId)
        {
            return items
                .Where(x => x.MenuChaId == menuChaId)
                 .OrderBy(x => x.ViTri)//__H__
                .Select(x => new ItemTreeView
                {
                    id = x.Id.ToString(),
                    text = x.TenMenu,
                    ModerationStatus = (Int32)x.ModerationStatus,
                    children = GetChildren(items, x.Id) // Lấy danh sách con của menu hiện tại
                })
                .ToList();
        }

        public MenuQuanTri_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuQuanTris
                        where obj.Id == Id
                        select new MenuQuanTri_Entity
                        {
                            Id = obj.Id,
                            TenMenu = obj.TenMenu,
                            Category = obj.Category,
                            Icon = obj.Icon,
                            ViTri = obj.ViTri,
                            LienKet = obj.LienKet,
                            MenuChaId = obj.MenuChaId,
                            MenuCha = context.MenuQuanTris.FirstOrDefault(x => x.Id == obj.MenuChaId)
                        };

            return query.FirstOrDefault();
        }

        public List<int> GetAllMenuQuanTriIds()
        {
            var context = (DBContext)UnitOfWork.Context;

            var distinctMenuChaIds = context.MenuQuanTris
                                        .Where(m => m.Category == true)
                                        .Select(m => m.Id)
                                        .ToList();

            return distinctMenuChaIds;
        }

        public bool CheckHaveSubItem(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuQuanTris
                        where obj.MenuChaId == Id
                        select obj;
            return query.Any();
        }
    }
}
