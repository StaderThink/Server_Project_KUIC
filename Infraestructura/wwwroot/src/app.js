Vue.component("slider", {
    created() {
        this.tabs = this.$slots.default.filter(n => n.tag);
        this.tabs.map((t, i) => t.key = i);
    },

    data() {
        return {
            tabs: [],
            selected: 0
        };
    },

    render: function(createElement) {
        const that = this;

        return createElement(
            "div",

            {
                class: { "slide": true },
            },

            [
                createElement(
                    "transition-group",

                    {
                        props: {
                            tag: "div",
                            name: "slide"
                        },
                    },

                    [this.tabs[this.selected]]
                ),

                createElement(
                    "div",

                    {
                        class: { "dots": true }
                    },

                    that.tabs.map(
                        (_, index) => {
                            return createElement(
                                "span",
            
                                {
                                    class: { "dot": true, "is-active": that.selected == index },
                                    key: index,
                                    on: {
                                        click: () => that.changeSelected(index)
                                    }
                                },
                            );
                        }
                    )
                )
            ],
        );
    },

    methods: {
        changeSelected(value = 0) {
            this.selected = value;
        }
    }
});

Vue.component("tab", {
    template: `
        <div class="tab content">
            <slot></slot>
        </div>
    `
})

new Vue({
    el: ".landing"
})