//function to show coin chart or alert form 
function togglediv(tab)
{
    document.getElementById('alert').classList.toggle('hidden', tab !== 'alerts'); 

    document.getElementById('chart').classList.toggle('hidden', tab !== 'coins'); 
}

togglediv('coins');




//function to display form specific input base on the user selection 
document.addEventListener('DOMContentLoaded', function(){
    function updateAlertFields() {
        const type = document.getElementById('alert-type').value;
        
        document.getElementById('price-fields').classList.toggle('hidden', type !== 'price');
        document.getElementById('ma-fields').classList.toggle('hidden', type !== 'ma');
        document.getElementById('rsi-fields').classList.toggle('hidden', type !== 'rsi');
    }
    document.getElementById('alert-type').addEventListener('change', updateAlertFields);
    // Set initial state
    updateAlertFields();

});

