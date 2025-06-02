function togglediv(tab)
{
    document.getElementById('alert').classList.toggle('hidden', tab !== 'alerts'); 

    document.getElementById('chart').classList.toggle('hidden', tab !== 'coins'); 
}

togglediv('alert');