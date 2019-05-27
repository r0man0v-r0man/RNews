var input = document.querySelector("textarea[name=tags2]"),
    tagify = new Tagify(input, {
        delimiters: ", ",
        maxTags: 6,
        duplicates: false,
    });