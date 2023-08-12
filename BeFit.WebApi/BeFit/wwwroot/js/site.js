function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7216/api/statistics', function (data) {
                $('#total_events').text(data.totalEvents + " Events");
                $('#total_clients').text((data.totalClients - data.totalCoaches) + " Clients");
                $('#total_coaches').text(data.totalCoaches + " Coaches");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text("Hide Statistics");
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        }
        else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text("Show Statistics");
            $('#statistics_btn').removeClass('btn-danger');
            $('#statistics_btn').addClass('btn-primary');
        }
    })
}
