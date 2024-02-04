
document.addEventListener('click', function (event) {
    var resultsContainer = document.getElementById('autocomplete-results');
    if (!resultsContainer.contains(event.target)) {
        resultsContainer.style.display = 'none';
    }
});

function showResults() {
    var input = document.getElementById('search-input');
    var resultsContainer = document.getElementById('autocomplete-results');
    var query = input.value;

    if (query.trim() === '') {
        resultsContainer.style.display = 'none';
        return;
    }

    $.ajax({
        type: 'GET',
        url: '/MusicAlbums/Search',
        data: { query: query },
        success: function (data) {
            resultsContainer.innerHTML = '';

            if (data.length > 0) {
                var resultList = document.createElement('ul');
                resultList.className = 'autocomplete-results-list';

                data.forEach(function (result) {
                    var listItem = document.createElement('li');
                    var link = document.createElement('a');
                    if (result.Id !== undefined) {
                        link.href = '/MusicAlbums/MusicPlay/' + result.Id;
                        link.textContent = `${result.Musicname}, Artist: ${result.Artist}`;
                        listItem.appendChild(link);
                        resultList.appendChild(listItem);
                    }
                });

                resultsContainer.appendChild(resultList);
                resultsContainer.style.display = 'block';
            } else {
                resultsContainer.style.display = 'none';
            }
        }
    });
}

