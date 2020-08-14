Vue.component("slider", {
    created() {
        this.tabs = this.$slots.default.filter(n => n.tag);
        this.tabs.map((t, i) => t.key = i);
    },

    mounted() {
        setInterval(
            () => {
                if (this.selected == (this.tabs.length - 1)) {
                    this.selected = 0;
                }

                else {
                    this.selected++;
                }
            },

            6000
        );
    },

    data() {
        return {
            tabs: [],
            selected: 0
        };
    },

    methods: {
        slideTab() {
            if (this.selected == (this.tabs.length - 1)) {
                this.selected = 0;
            }

            else {
                this.selected++;
            }
        }
    },

    render: function(createElement) {
        return createElement(
            "div",

            {
                class: { "slider": true },
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

                    this.tabs.map(
                        (_, index) => {
                            return createElement(
                                "span",
            
                                {
                                    class: { "dot": true, "is-active": this.selected == index },
                                    key: index,
                                    on: {
                                        click: () => this.changeSelected(index)
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
});

function loadSlidingPage() {
    new Vue({
        el: ".sliding"
    });
}
