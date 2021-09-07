import { FuseNavigation } from '@fuse/types';

export const navigationAdmin: FuseNavigation[] = [
    {
        id: 'applications',
        title: 'Applications',
        translate: 'NAV.APPLICATIONS',
        type: 'group',
        icon: 'apps',
        children: [
            {
                id: 'dashboards',
                title: 'منو',
                translate: 'NAV.DASHBOARDS',
                type: 'collapsable',
                icon: 'dashboard',
                children: [
                    {
                        id: 'myfault',
                        title: 'اعلام خرابی',
                        type: 'item',
                        url: '/apps/myfault'
                    },
                    {
                        id: 'fixfault',
                        title: 'کارتابل رفع کننده خرابی',
                        type: 'item',
                        url: '/apps/fixfault'
                    }

                ]
            },
            {
                id: 'usermanager',
                title: 'مدیریت',
                translate: 'NAV.DASHBOARDS',
                type: 'collapsable',
                icon: 'people',
                children: [
                    {
                        id: 'usermanager',
                        title: 'مدیریت کاربران',
                        type: 'item',
                        url: '/apps/usermanager'
                    },
                    {
                        id: 'location',
                        title: 'مدیریت مکان',
                        type: 'item',
                        url: '/apps/location'
                    },
                    {
                        id: 'fixUnit',
                        title: 'واحد تعمیر کننده',
                        type: 'item',
                        url: '/apps/fixunit'
                    },
                    {
                        id: 'part',
                        title: 'قطعات',
                        type: 'item',
                        url: '/apps/part'
                    },
                    {
                        id: 'itemtype',
                        title: 'مدیریت نوع',
                        type: 'item',
                        url: '/apps/itemtype'
                    },
                    {
                        id: 'faulttype',
                        title: 'نوع خرابی',
                        type: 'item',
                        url: '/apps/faulttype'
                    }
                ]
            }
        ]
    }
];
export const navigationNormal: FuseNavigation[] = [
    {
        id: 'applications',
        title: 'Applications',
        translate: 'NAV.APPLICATIONS',
        type: 'group',
        icon: 'apps',
        children: [
            {
                id: 'dashboards',
                title: 'داشبورد',
                translate: 'NAV.DASHBOARDS',
                type: 'collapsable',
                icon: 'dashboard',
                children: [
                    {
                        id: 'myfault',
                        title: 'اعلام خرابی',
                        type: 'item',
                        url: '/apps/myfault'
                    },
                    {
                        id: 'fixfault',
                        title: 'کارتابل رفع کننده خرابی',
                        type: 'item',
                        url: '/apps/fixfault'
                    }

                ]
            }
        ]
    }
];
export const navigationNone: FuseNavigation[] = [
    {
        id: 'applications',
        title: 'Applications',
        translate: 'NAV.APPLICATIONS',
        type: 'group',
        icon: 'apps',
        children: [
            
        ]
    }
];
