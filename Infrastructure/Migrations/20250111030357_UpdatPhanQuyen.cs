using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatPhanQuyen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuQuanTris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMenu = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LienKet = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MenuChaId = table.Column<int>(type: "int", nullable: true),
                    ViTri = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuQuanTris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TokenJira = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhTrangDocThan = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuyenSuDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyenSuDungCha = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyenSuDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuNguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuNguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuNguoiDungs_MenuQuanTris_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MenuQuanTris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuNguoiDungs_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    keys = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDungSubscriptions_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyenNguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuyenId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyenNguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanQuyenNguoiDungs_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanQuyenNguoiDungs_QuyenSuDungs_QuyenId",
                        column: x => x.QuyenId,
                        principalTable: "QuyenSuDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaiTroId = table.Column<int>(type: "int", nullable: false),
                    MenuQuanTriId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaiTroMenus_MenuQuanTris_MenuQuanTriId",
                        column: x => x.MenuQuanTriId,
                        principalTable: "MenuQuanTris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroMenus_VaiTros_VaiTroId",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroNguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VaiTroId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroNguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDungs_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDungs_VaiTros_VaiTroId",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroQuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaiTroId = table.Column<int>(type: "int", nullable: false),
                    QuyenSuDungId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroQuyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaiTroQuyens_QuyenSuDungs_QuyenSuDungId",
                        column: x => x.QuyenSuDungId,
                        principalTable: "QuyenSuDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroQuyens_VaiTros_VaiTroId",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuNguoiDungs_MenuId",
                table: "MenuNguoiDungs",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuNguoiDungs_UserId",
                table: "MenuNguoiDungs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungSubscriptions_UserId",
                table: "NguoiDungSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyenNguoiDungs_QuyenId",
                table: "PhanQuyenNguoiDungs",
                column: "QuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyenNguoiDungs_UserId",
                table: "PhanQuyenNguoiDungs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroMenus_MenuQuanTriId",
                table: "VaiTroMenus",
                column: "MenuQuanTriId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroMenus_VaiTroId",
                table: "VaiTroMenus",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_UserId",
                table: "VaiTroNguoiDungs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_VaiTroId",
                table: "VaiTroNguoiDungs",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroQuyens_QuyenSuDungId",
                table: "VaiTroQuyens",
                column: "QuyenSuDungId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroQuyens_VaiTroId",
                table: "VaiTroQuyens",
                column: "VaiTroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuNguoiDungs");

            migrationBuilder.DropTable(
                name: "NguoiDungSubscriptions");

            migrationBuilder.DropTable(
                name: "PhanQuyenNguoiDungs");

            migrationBuilder.DropTable(
                name: "VaiTroMenus");

            migrationBuilder.DropTable(
                name: "VaiTroNguoiDungs");

            migrationBuilder.DropTable(
                name: "VaiTroQuyens");

            migrationBuilder.DropTable(
                name: "MenuQuanTris");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "QuyenSuDungs");

            migrationBuilder.DropTable(
                name: "VaiTros");
        }
    }
}
