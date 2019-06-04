var input = document.querySelector("textarea[name=Tags]"),
    tagify = new Tagify(input, {
        delimiters: ", ",
        maxTags: 6,
        duplicates: false,
    });