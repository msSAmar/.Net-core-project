function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPassword=document.getElementById('add-password');
    const addIsAdmin=document.getElementById('add-isAdmin');

    const item = {
        UserName: addNameTextbox.value.trim(),
        Password:addPassword.value.trim(),
        IsAdmin:addIsAdmin.value.trim()
    };

    fetch(uriUser, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            addUsers();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}