import Vue from 'vue'
import VueI18n from 'vue-i18n'

Vue.use(VueI18n)
function loadLocaleMessages() {
    //get data from local storage
    const locales = require.context('./locales', true, /[A-Za-z0-9-_,\s]+\.json$/i)
    const messages = {}
    locales.keys().forEach(key => {
        const matched = key.match(/([A-Za-z0-9-_]+)\./i)
        if (matched && matched.length > 1) {
            const locale = matched[1]
            messages[locale] = locales(key)
        }
    })
    return messages
}

function detectLanguage() {
    const lng = window.navigator.userLanguage || window.navigator.language;
    const locales = require.context(
        "./locales",
        true,
        /[A-Za-z0-9-_,\s]+\.json$/i
    );
    const lang = locales
        .keys()
        .find((key) => lng.includes(key.replace("./", "").replace(".json", "")));
    return lang ? lang.replace("./", "").replace(".json", "") : null;
}

export default new VueI18n({
    locale: localStorage.getItem("lang") ||
        detectLanguage() ||
        'vi',
    fallbackLocale: 'vi',
    messages: loadLocaleMessages(),
})

