$(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/taskHub").build();

    connection.start();

    $("#add-chore").on('click', () => {
        const name = $('#name').val();
        connection.invoke("AddChore", { name });
    });

    connection.on('NewChore', chore => {
        $("#chore-table").append(`<tr id="chore-${chore.id}"><td>${chore.name}</td><td><button class="btn btn - success accept" data-chore-id="${ chore.id }">I'll Accept This Chore!</button></td></tr>`)
    });

    $('#chore-table').on('click', '.accept', function() {
        const chore = {
            id: $(this).data('chore-id'),
            user: {
                email: $('#userEmail').val()
            }
        };
       
        connection.invoke('AcceptChore', chore)
    });

    connection.on('SomeoneAcceptedChore', chore => {
        $(`#chore-${chore.id}`).find('td:eq(1)').html(`<td><button class="btn btn-danger" disabled>Task being completed by: ${chore.user.firstName} ${chore.user.lastName}</button></td>`);

    });

    connection.on('IAcceptedChore', chore => {
        console.log('accepted chore');
        $(`#chore-${chore.id}`).find('td:eq(1)').html(`<td><button class="btn btn-info completed" data-chore-id=@c.Id>I Finished!</button></td>`);

    });

    $('#chore-table').on('click', '.completed', function () {

        const chore = {
            id: $(this).data('chore-id')
        };

        connection.invoke('completedchore', chore);

    })

    connection.on('RemoveChore', chore => {
        $(`#chore-${chore.id}`).remove();

    });






});
