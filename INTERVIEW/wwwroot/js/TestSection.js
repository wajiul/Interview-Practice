var currentIndex = 0;
var stringList = @Html.Raw(Json.Serialize(Model));
console.log(stringList);
function Show() {
    if (currentIndex >= stringList.length) {
        UpdateToDatabase();
        currentIndex = 0;
        return;
    }

    var stringDisplay = document.getElementById("string-display");
    console.log(stringList[0]);
    stringDisplay.innerHTML = stringList[currentIndex]["question"];
}
function Next() {
    stringList[currentIndex]["rank"] -= 1;
    currentIndex++;
    Show();
}
function Skip() {
    stringList[currentIndex]["rank"] += 2;
    currentIndex++;
    Show();
}
function Average() {
    stringList[currentIndex]["rank"] += 1;
    currentIndex++;
    Show();
}

function UpdateToDatabase() {
    console.log("update to database");
    console.log(stringList);
    $.post("Test/PostQuestions", { "questions": `${stringList}` }, function (stringList) {
        let op_msg = "Successful";
        if (data.status == false) {
            op_msg = "Failed";
        }
    });
}

Show();

