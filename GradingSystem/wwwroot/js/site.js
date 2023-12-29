// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(document).ready(function () {
//    $(".course-link").click(function (e) {
//        e.preventDefault(); // Sayfanın yönlendirme işlemini durdur

//        var courseId = $(this).data('courseid'); // jQuery kullanarak data attributelerine ulaşma

//        // AJAX ile veriyi sunucuya gönder
//        $.ajax({
//            type: 'POST',
//            url: '/Home/MyViewData', // İlgili Controller ve Action'ın adını belirtin
//            data: { courseId: courseId },
//            success: function (result) {
//                // İşlem başarılıysa burada gerekli işlemleri yapabilirsiniz
//                console.log(result);
//            },
//            error: function (error) {
//                // Hata durumunda burada işlemleri yapabilirsiniz
//                console.error(error);
//            }
//        });
//    });
//});