$(document).ready(function () {
    $(".btn-report").click(function () {
        hideAllForms();
        $("#reportForm").show();
    });

    $(".btn-projectAdd").click(function () {
        hideAllForms();
        $("#projectAddForm").show();
    });

    $(".btn-quiz").click(function () {
        hideAllForms();
        $("#quizForm").show();
    });
    $(".btn-quizAdd").click(function () {
        hideAllForms();
        $("#quizAddForm").show();
    });

    $(".btn-project").click(function () {
        hideAllForms();
        $("#projectForm").show();
    });

    $(".closeBtn").click(function () {
        $(this).closest('.form-container').hide();
    });

    $(document).ready(function () {
        var isSubmitting = false;
        $("#addReportBtn").click(function (e) {
            var reportNameValue = document.getElementById('reportName').value;
            var reportValue = document.getElementById('report').value;
            if (!reportNameValue.trim() || !reportValue.trim()) {
                alert('lütfen tüm gerekli alanları doldurunuz');
                return false;
            }
            else {
                var studentId = selectedStudentId;

                var formData = {
                    StudentReport: {
                        ReportName: $("#reportName").val(),
                        Report: $("#report").val(),
                        StudentID: studentId
                    }
                };

                e.stopImmediatePropagation();
                $.ajax({
                    dataType: "json",
                    url: '/Studentpage/AddReport',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(formData),
                    success: function (data) {
                        alert('Başarıyla eklendi.');

                    },
                    error: function (error) {
                        console.error('Gönderme hatası:', error);

                    }
                });

                $.ajax({
                    type: 'GET',
                    url: '/Studentpage/RefreshCard',
                    success: function (data) {
                        $("#cards").html(data);
                    },
                    error: function () {
                        alert('Veriler güncellenirken bir hata oluştu.');
                    }
                });
                document.getElementById('reportName').value = null;
                document.getElementById('report').value = null;
                
            }


        });
    });

    $(document).ready(function () {

        $("#addQuizBtn").click(function (e) {

            var studentId = selectedStudentId;

            var formData = {
                StudentQuiz: {
                    QuizID: $("#quizName").val(),
                    Description: $("#quizDescription").val(),
                    StudentID: studentId
                },
                StudentMark: {
                    Mark: $("#quizMark").val()
                }
            };

            e.stopImmediatePropagation();
            $.ajax({
                dataType: "json",
                url: '/Studentpage/AddQuizMark',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(formData),
                success: function (data) {
                    console.log('Başarıyla gönderildi:', data);

                },
                error: function (error) {
                    console.error('Gönderme hatası:', error);

                }
            });
            $("#quizForm").hide();
        });
    });



});


$(document).ready(function () {
    $("#addProjectBtn").click(function (e) {
        var studentId = selectedStudentId;

        var formData = {
            StudentProject: {
                ProjectID: $("#projectName").val(),
                Description: $("#projectDescription").val(),
                StudentID: studentId
            },
            StudentMark: {
                Mark: $("#projectMark").val()
            }
        };

        e.stopImmediatePropagation();
        $.ajax({
            dataType: "json",
            url: '/Studentpage/AddProjectMark',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(formData),
            success: function (data) {
                console.log('Başarıyla gönderildi:', data);

            },
            error: function (error) {
                console.error('Gönderme hatası:', error);

            }
        });
        $("#projectForm").hide();
    });

});


function hideAllForms() {
    $(".form-container").hide();
}




var selectedStudentId = null;

function setStudentId(studentId) {
    selectedStudentId = null;
    selectedStudentId = studentId;

}


function updateStudentReportPage() {
    // AJAX ile server'dan güncel veriyi al
    $.ajax({
        type: "GET",
        url: '/StudentViewComponent/Invoke', // Güncel veriyi getirecek metodunuzu belirtin
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var newContent = "";

            for (var i = 0; i < data.length; i++) {
                var student = data[i];
                newContent += "<li>" +
                    "<div class='card'>" +
                    "<div class='card-body'>" +
                    "<h5 class='card-title text-center'>" + student.StudentName + " " + student.StudentSurname + "</h5>" +
                    "<p class='card-text'>Bugün Girilen Rapor Sayisi : " + student.StudentReport.Count + "</p>";

                if (student.StudentReport.Count >= 1) {
                    var lastReport = student.StudentReport.Reports[student.StudentReport.Reports.length - 1];
                    newContent += "<p class='card-text'>Soru : " + lastReport.ReportName +
                        " Son Raporlama : " + lastReport.Report + "</p>";
                }

                newContent += "</div>" +
                    "<div class='card-body medium text-center'>" +
                    "<a href='#' class='card-link row-cols-sm-2 btn-project'>Project</a>" +
                    "<a href='#' class='card-link row-cols-sm-2 btn-quiz'>Quiz</a>" +
                    "<a href='#' onclick='setStudentId(" + student.StudentID + ");' class='card-link row-cols-sm-2 btn-report'>Report</a>" +
                    "</div>" +
                    "</div>" +
                    "</li>";
            }

            // Sayfa içeriğini güncelle
            $("ol").html(newContent);
        },
        error: function (error) {
            console.error('Güncelleme hatası:', error);
        }
    });
}