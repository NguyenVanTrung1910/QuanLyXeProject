namespace Shared
{
    public enum StatusCode
    {
        // 1xx: Thông tin
        ThongTin = 100,
        TiepTuc = 100,
        ChuyenGiao = 101,
        DangXuLy = 102,

        // 2xx: Thành công
        ThanhCong = 200,
        DaTao = 201,
        DaChapNhan = 202,
        ThongTinKhongCoQuyen = 203,
        KhongCoNoiDung = 204,
        DatLaiNoiDung = 205,

        // 3xx: Chuyển hướng
        NhieuLuaChon = 300,
        DiChuyenVinhVien = 301,
        TimThay = 302,
        XemMucKhac = 303,
        KhongThayDoi = 304,
        SuDungProxy = 305,
        ChuyenHuongTamThoi = 307,
        ChuyenHuongVinhVien = 308,

        // 4xx: Lỗi của client
        YeuCauKhongHopLe = 400,
        KhongDuocPhep = 401,
        CanThanhToan = 402,
        BiCam = 403,
        KhongTimThay = 404,
        PhuongThucKhongChapNhan = 405,
        KhongChapNhan = 406,
        CanXacThucProxy = 407,
        YeuCauHetHan = 408,
        XungDot = 409,
        DaMatDi = 410,
        CanCoDoDai = 411,
        DieuKienTienQuyetThatBai = 412,
        ThucTheYeuCauQuaLon = 413,
        URIQuaDai = 414,
        LoaiPhuongTienKhongDuocHoTro = 415,
        KhongTheSapXep = 416,
        TrangThaiThatBai = 417,
        TaiNguyenLa = 418, // Trà hay cà phê :)
        QuaThoi = 426,

        // 5xx: Lỗi của server
        LoiMaychu = 500,
        ChuaThucHien = 501,
        CongKhongHopLe = 502,
        DichVuKhongKhaDung = 503,
        CongHetThoiGian = 504,
        PhienBanHTTPKhongDuocHoTro = 505,

        // Mã lỗi tùy chỉnh
        LoiCauHinhHeThong = 600,
        LoiKetNoiCSDL = 601,
        LoiXuLyDuLieu = 602,
        LoiBaoMatUngDung = 603,
        LoiHieuSuatUngDung = 604,
        LoiTichHopHeThong = 605,

        // Có thể thêm các mã lỗi tùy chỉnh khác ở đây
    }
}
