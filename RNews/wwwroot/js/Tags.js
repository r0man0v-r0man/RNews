var input = document.querySelector("textarea[name=tags2]"),
    tagify = new Tagify(input, {
        delimiters: ", ",
        maxTags: 6,
        duplicates: false,
    });
document.querySelector("tags").classList.add("form-control", "mb-3");