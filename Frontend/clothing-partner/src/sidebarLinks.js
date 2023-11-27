export default [
  {
    name: 'Dashboard',
    icon: 'nc-icon nc-bank',
    path: '/admin/overview'
  },
  {
    name: 'Employees',
    icon: 'nc-icon nc-book-bookmark',
    children: [
 
      {
        name: 'Create User / Employee',
        path: '/pages/createUser' 
      },
      
    ]
  },
] 
