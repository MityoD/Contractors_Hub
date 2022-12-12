# Contractors_Hub
SoftUni ASP.NET Advanced 2022 Project

The project configure three users with corespondig roles:  

>username: guest password: guest  
>username: contractor password: contractor  
>username: admin password: admin  
  
Guest:  
>Guest role is set on user registration  
>Guests can: 
>>Add jobs  
>>>The job is sent for admin review and can be accepted or declined.  
>>>User can Edit job before it is Accepted from the Admin  
>>>If the job is Taken or Accepted it can not be deleted  
  
>>Receive offers  
>>>User can receive offer for his job(Jobs/My Job Offers - one offer from contractor)  
>>>Offers can be accepted or declined:  
>>>>If offer is accepted the current job is marked as "Taken"
>>>>When the job is completed guest can mark the job as "Completed" from Jobs/My Jobs and rate the contractor.  

>>Search tools  
>>>Tools can be added to user's cart where quantity is selected, address is required to submit the order. From the menu button the user can review his order status.  

>>View all contractors list from the menu  
>>>if the guest want to become contractor from the Contractors/Become button he is required to enter his First name, Last name and phone number. On submit the Guest changes his role to Contractor and can access the Contractor options.  

Contractor:  
>Guest can become contractor from Contractors/Become button  
>Contractors can save offers for all available jobs in the Jobs menu  
>Search tools  
>>Tools can be added to user's cart where quantity is selected, address is required to submit the order. From the menu button the user can review his order status.  

Admin:  
>Can access Administration page:  
>>Add/Edit/Accept/Decline Jobs  
>>Add/Edit/Delete Tools
>>Review Orders and mark them as Dispatched  
>>View Jobs statistics
