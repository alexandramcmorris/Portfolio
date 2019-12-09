//holds the json object
var json_words;

//need to have the array start new
var word_array = new Array(5);

//starts at 4 so when you get rid of one it doesn't repeat the first 5 words in the json
var count = 4;

//holds the url used to get the words
var requestURL = 'DictionaryWords.php';

//opens the connection to get the json from the php
var request = new XMLHttpRequest();
request.open('GET', requestURL);
request.responseType = 'json';
request.send();

//loads the json object
request.onload = function() {
    json_words = request.response;
    loadFirstWords();
}

//loads the json into the arrays
function loadFirstWords() {
    
    for (var j = 0; j < 5; j++)
    {
        word_array[j] = json_words[j].word;
    }
}


///this requires which spot is opened. it needs the array index number
function wordDone(arrayNumber)
{
    var arraycount = arrayNumber;
    var key = word_array[arraycount];
    json_words[key] = null;
    delete json_words[key];
    count++;
    word_array[arraycount] = json_words[count].word;
    
	//for these the document.getElementById needs to be changed to where ever
	//the word is going
    switch (arraycount) {
        case 0:
            document.getElementById("word1").innerHTML = word_array[0];
            break;
        case 1:
            document.getElementById("word2").innerHTML = word_array[1];
            break;
        case 2:
            document.getElementById("word3").innerHTML = word_array[2];
            break;
        case 3:
            document.getElementById("word4").innerHTML = word_array[3];
            break;
        case 4:
            document.getElementById("word5").innerHTML = word_array[4];
            break;
        default:
            break;
    }    
}