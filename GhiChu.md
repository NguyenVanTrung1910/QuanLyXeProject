
**Mô tả cấu trúc project**
-Solution Items
-src
  |- Core
      |- SIDVC.Domain
          |- SIDVC.Domain.Entities  //chứa các đối tượng sử dụng trong hệ thống
          |- SIDVC.Domain.Repositories //Chưa các interface của các repository
      |- SIDVC.Application
          |- SIDVC.Application.DTOs  //chứa các đối tượng chuyên đổi với mục đích riêng
          |- SIDVC.Application.Interfaces  //Chưa các interface của các các service
          |- SIDVC.Application.Services  //Chứa nghiệp vụ services cụ thể ứng với các UserCase, Chứa các logic nghiệp vụ liên quan đến xử lý ứng dụng, đảm bảo tương tác đúng với các entity từ domain và repository.
      |- SIDVC.Share  //chứa các lớp dùng chung. dùng lại nhiều lần. thuạt toán riêng, vv..
  |- Infrastructure
          |- SIDVC.Infrastructure
              |- SIDVC.Infrastructure.Repositorys  //chưa các lớp khởi tạo vào list lấy dữ liệu
          |- SIDVC.Infrastructure.DependencyInjection  //Chứa cấu hình DI trên từ SIDVC.Infrastructure.Repositorys vào Unity
      |- Presentation
          |- SIDVC.CMS
              |- SIDVC.CMS.Content  //chứa tài nguyên css, ảnh
              |- SIDVC.CMS.Controllers  //chứa controller xử lý yêu cầu của người dùng
              |- SIDVC.CMS.DependencyResolution  
                  |- SIDVC.CMS.DependencyResolution.UnityConfig  //chứa nơi đăng ký DI service
              |- SIDVC.CMS.Views        chứa các view của controller 
          |- SIDVC.Console      //viết thử nghiệm gì đó vào đây
          |- SIDVC.Pubishing    //chứa code khai thác
              |- SIDVC.Pubishing.UserControls    //Mapping vào cổng để nhận code 
              |- SIDVC.Pubishing.UserControls.Content       //chứa tài nguyên tĩnh  
              |- SIDVC.Pubishing.UserControls.Publishing    //chứa tài code userControll
              |- SIDVC.Pubishing.WebParts       //Chưa webpart đăng ký 
- test





## Lưu ý về references
## Presentation Layer chỉ có thể tham chiếu đến Application Layer.
## Application Layer có thể tham chiếu đến Domain Layer và gọi Infrastructure Layer thông qua interface.
## Domain Layer không tham chiếu đến bất kỳ lớp nào khác.
## Infrastructure Layer chỉ nên được gọi gián tiếp từ Application Layer qua các interface (được truyền vào từ ngoài).
