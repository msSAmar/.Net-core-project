const uri = '/Admin';
function addItem() {
    const NameTextbox = document.getElementById('name');
    const PasswordTextbox = document.getElementById('password');

    const item = {
        Name:NameTextbox.value,
        Password:PasswordTextbox.value
,       
    };

    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
           
            NameTextbox.value = '';
            PasswordTextbox.value='';
        })
        .catch(error => console.error('Unable to add item.', error));
}
